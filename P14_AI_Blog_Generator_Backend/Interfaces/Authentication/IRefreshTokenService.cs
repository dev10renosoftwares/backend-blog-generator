
using P14_AI_Blog_Generator_Backend.Models.DomainModels;

namespace P14_AI_Blog_Generator_Backend.Interfaces;

public interface IRefreshTokenService
{
    string GenerateRefreshToken();

    Task SaveRefreshTokenAsync(User user, string refreshToken);

    Task<RefreshToken?> GetRefreshTokenAsync(string token);

    Task RevokeRefreshTokenAsync(string token);

    Task ReplaceRefreshTokenAsync(
        RefreshToken existingToken,
        string newToken);
}