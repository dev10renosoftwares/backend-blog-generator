


namespace P14_AI_Blog_Generator_Backend.ApiResponse;

public class ApiResponse<T>
{
    public bool Success { get; set; }

    public string Message { get; set; } = string.Empty;

    public T? Data { get; set; }

    public List<string>? Errors { get; set; }
}