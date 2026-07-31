using P14_AI_Blog_Generator_Backend.Models.DomainModels;

namespace P14_AI_Blog_Generator_Backend.Interfaces;

public interface IJwtService
{
    string GenerateAccessToken(User user);

    DateTime GetAccessTokenExpiry();
}