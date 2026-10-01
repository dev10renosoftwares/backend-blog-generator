using System.Text.RegularExpressions;
using BlogGenerator.Interfaces.KeywordRestriction;
using BlogGenerator.ServiceModels.v1.KeywordRestriction;

namespace BlogGenerator.BAL.KeywordRestriction;

public class KeywordRestrictionService : IKeywordRestrictionService
{
    private readonly ILogger<KeywordRestrictionService> _logger;

    private static readonly string[] RestrictedKeywords =
{
    // Violence / serious harm
    "kill",
    "killing",
    "murder",
    "murderer",
    "murdering",
    "assassinate",
    "assassination",
    "execute",
    "execution",
    "massacre",
    "slaughter",
    "behead",
    "beheading",
    "decapitate",
    "decapitation",
    "dismember",
    "dismemberment",
    "torture",
    "torturing",
    "brutalize",
    "brutality",
    "stab",
    "stabbing",
    "shoot",
    "shooting",
    "shootout",
    "strangle",
    "strangulation",
    "choke",
    "choking",
    "hang",
    "hanging",
    "burn alive",

    // Threats / harm
    "threaten",
    "threat",
    "threatening",
    "assault",
    "attack",
    "attacking",
    "terrorize",
    "terrorize",
    "harm",
    "hurt",
    "injure",
    "injury",
    "poison",
    "poisoning",

    // Weapons
    "bomb",
    "bombing",
    "explosive",
    "explosives",
    "grenade",
    "detonate",
    "detonation",
    "weapon",
    "firearm",
    "rifle",
    "pistol",
    "gun",
    "ammunition",

    // Sexual exploitation / explicit sexual content
    "rape",
    "raping",
    "sexual assault",
    "sexual abuse",
    "child abuse",
    "child sexual abuse",
    "sexual exploitation",
    "sex trafficking",
    "human trafficking",
    "prostitution",
    "incest",
    "pedophilia",
    "pedophile",
    "pornography",
    "pornographic",
    "explicit sex",

    // Drugs / serious illegal activity
    "cocaine",
    "heroin",
    "methamphetamine",
    "meth",
    "fentanyl",
    "crack cocaine",
    "drug trafficking",
    "drug dealing",
    "drug dealer",

    // Terrorism / extremist violence
    "terrorism",
    "terrorist attack",
    "terrorist",
    "suicide bombing",
    "suicide bomber",
    "extremist attack",
    "violent extremism",
    "extremist recruitment"
};

    public KeywordRestrictionService(
        ILogger<KeywordRestrictionService> logger)
    {
        _logger = logger;
    }

    public bool ContainsRestrictedKeyword(string text)
    {
        return CheckText(text).IsAllowed == false;
    }

    public KeywordCheckResult CheckText(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            return new KeywordCheckResult
            {
                IsAllowed = true
            };
        }

        var normalizedText = NormalizeText(text);

        var violations = new List<string>();

        foreach (var keyword in RestrictedKeywords)
        {
            var normalizedKeyword = NormalizeText(keyword);

            if (string.IsNullOrWhiteSpace(normalizedKeyword))
                continue;

            var pattern = $@"(?<!\w){Regex.Escape(normalizedKeyword)}(?!\w)";

            if (Regex.IsMatch(
                normalizedText,
                pattern,
                RegexOptions.IgnoreCase))
            {
                violations.Add(keyword);
            }
        }

        if (violations.Count > 0)
        {
            _logger.LogWarning(
                "Restricted keyword detected. Count: {Count}",
                violations.Count);

            return new KeywordCheckResult
            {
                IsAllowed = false,
                Message = "Your text contains restricted words. Please remove them and try again.",
                Violations = violations
            };
        }

        return new KeywordCheckResult
        {
            IsAllowed = true
        };
    }

    private static string NormalizeText(string text)
    {
        return Regex.Replace(
                text.ToLowerInvariant(),
                @"\s+",
                " ")
            .Trim();
    }
}