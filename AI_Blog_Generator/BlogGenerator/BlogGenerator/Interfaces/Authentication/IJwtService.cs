using BlogGenerator.DomainModels.v1;

namespace BlogGenerator.Interfaces.Authentication;

public interface IJwtService
{
    string GenerateAccessToken(User user);

    DateTime GetAccessTokenExpiry();
}