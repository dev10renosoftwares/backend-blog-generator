using System.Text.Json;
using BlogGenerator.Interfaces;
using BlogGenerator.Interfaces.ContentModeration;
using BlogGenerator.ServiceModels.v1.ContentModeration;

namespace BlogGenerator.BAL.ContentModeration;

public class ContentModerationService : IContentModerationService
{
    private readonly IAIProviderService _aiProviderService;
    private readonly ILogger<ContentModerationService> _logger;

    public ContentModerationService(
        IAIProviderService aiProviderService,
        ILogger<ContentModerationService> logger)
    {
        _aiProviderService = aiProviderService;
        _logger = logger;
    }

    public async Task<AIContentScanResult> ScanBlogContentAsync(
        string title,
        string content)
    {
        if (string.IsNullOrWhiteSpace(title) &&
            string.IsNullOrWhiteSpace(content))
        {
            return new AIContentScanResult
            {
                IsSafe = false,
                Reason = "Blog content cannot be empty.",
                Violations = new List<string>
                {
                    "Empty content"
                }
            };
        }

        var prompt =
      "You are an AI content moderation system.\n\n" +

      "Analyze the following blog before it is published.\n\n" +

      "Determine whether the blog contains clearly unsafe or " +
      "policy-violating content.\n\n" +

      "Check for:\n" +
      "1. Explicit sexual content.\n" +
      "2. Sexual content involving minors.\n" +
      "3. Graphic violence or glorification of violence.\n" +
      "4. Serious threats or encouragement of harm.\n" +
      "5. Instructions that facilitate serious wrongdoing.\n" +
      "6. Terrorism or extremist promotion, recruitment, or praise.\n" +
      "7. Hate or dehumanizing content targeting protected groups.\n" +
      "8. Dangerous criminal instructions.\n" +
      "9. Other clearly harmful or unsafe content.\n\n" +

      "Do NOT mark content unsafe merely because it discusses:\n" +
      "- History\n" +
      "- News\n" +
      "- Politics\n" +
      "- Science\n" +
      "- Medicine\n" +
      "- Fiction\n" +
      "- Violence in an educational context\n" +
      "- Difficult or controversial subjects\n" +
      "- General opinions\n" +
      "- Social issues\n\n" +

      "Evaluate the actual context of the content.\n\n" +

      "Return ONLY valid JSON.\n" +
      "Do not use markdown.\n" +
      "Do not wrap the JSON in ```.\n\n" +

      "The JSON must have exactly this structure:\n\n" +

      "{\n" +
      "    \"isSafe\": true,\n" +
      "    \"reason\": \"\",\n" +
      "    \"violations\": []\n" +
      "}\n\n" +

      "If the content violates the safety requirements, return:\n\n" +

      "{\n" +
      "    \"isSafe\": false,\n" +
      "    \"reason\": \"Short explanation of the violation.\",\n" +
      "    \"violations\": [\n" +
      "        \"Violation category\"\n" +
      "    ]\n" +
      "}\n\n" +

      "BLOG TITLE:\n" +
      title +
      "\n\n" +

      "BLOG CONTENT:\n" +
      content;

        try
        {
            _logger.LogInformation(
                "Starting AI content moderation scan for blog title: {Title}",
                title);

            var aiResponse =
                await _aiProviderService.GenerateBlogAsync(prompt);

            if (string.IsNullOrWhiteSpace(aiResponse))
            {
                _logger.LogError(
                    "AI content moderation returned an empty response.");

                return new AIContentScanResult
                {
                    IsSafe = false,
                    Reason = "Unable to verify blog content."
                };
            }

            var jsonResponse = CleanJsonResponse(aiResponse);

            var result =
                JsonSerializer.Deserialize<AIContentScanResult>(
                    jsonResponse,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

            if (result == null)
            {
                _logger.LogError(
                    "AI content moderation returned an invalid result.");

                return new AIContentScanResult
                {
                    IsSafe = false,
                    Reason = "Unable to verify blog content."
                };
            }

            _logger.LogInformation(
                "AI content moderation completed. Safe: {IsSafe}",
                result.IsSafe);

            return result;
        }
        catch (JsonException ex)
        {
            _logger.LogError(
                ex,
                "Failed to parse AI content moderation response.");

            return new AIContentScanResult
            {
                IsSafe = false,
                Reason = "Unable to verify blog content."
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "AI content moderation failed.");

            return new AIContentScanResult
            {
                IsSafe = false,
                Reason = "Content moderation service is currently unavailable."
            };
        }
    }

    private static string CleanJsonResponse(string response)
    {
        response = response.Trim();

        // Handle ```json ... ``` response just in case Gemini
        // returns markdown despite being instructed not to.
        if (response.StartsWith("```"))
        {
            var firstNewLine = response.IndexOf('\n');

            if (firstNewLine >= 0)
            {
                response = response[(firstNewLine + 1)..];
            }

            if (response.EndsWith("```"))
            {
                response = response[..^3];
            }

            response = response.Trim();
        }

        return response;
    }
}