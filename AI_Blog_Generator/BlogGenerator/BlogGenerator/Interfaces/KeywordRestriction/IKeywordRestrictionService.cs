using BlogGenerator.ServiceModels.v1.KeywordRestriction;

namespace BlogGenerator.Interfaces.KeywordRestriction;

public interface IKeywordRestrictionService
{
    KeywordCheckResult CheckText(string text);

    bool ContainsRestrictedKeyword(string text);
}