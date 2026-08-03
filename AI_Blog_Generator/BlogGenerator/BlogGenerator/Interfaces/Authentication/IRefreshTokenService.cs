using BlogGenerator.DomainModels.v1;

namespace BlogGenerator.Interfaces.Authentication;

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