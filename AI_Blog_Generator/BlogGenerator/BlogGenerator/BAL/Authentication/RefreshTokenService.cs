using BlogGenerator.DAL;
using BlogGenerator.DomainModels.v1;
using BlogGenerator.Interfaces.Authentication;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace BlogGenerator.BAL.Authentication;

public class RefreshTokenService : IRefreshTokenService
{
    private readonly ApplicationDbContext _context;
    private readonly IConfiguration _configuration;
    private readonly ILogger<RefreshTokenService> _logger;

    public RefreshTokenService(
        ApplicationDbContext context,
        IConfiguration configuration,
        ILogger<RefreshTokenService> logger)
    {
        _context = context;
        _configuration = configuration;
        _logger = logger;
    }

    public string GenerateRefreshToken()
    {
        var randomBytes = new byte[64];

        using var rng = RandomNumberGenerator.Create();

        rng.GetBytes(randomBytes);

        return Convert.ToBase64String(randomBytes);
    }

    public async Task SaveRefreshTokenAsync(
        User user,
        string refreshToken)
    {
        var expiryDays = Convert.ToInt32(
            _configuration["Jwt:RefreshTokenExpiryDays"] ?? "7");

        var token = new RefreshToken
        {
            UserId = user.UserId,
            Token = refreshToken,
            ExpiryDate = DateTime.UtcNow.AddDays(expiryDays),
            IsRevoked = false,
            CreatedAt = DateTime.UtcNow
        };

        _context.RefreshTokens.Add(token);

        await _context.SaveChangesAsync();

        _logger.LogInformation(
            "Refresh token saved for User {UserId}",
            user.UserId);
    }

    public async Task<RefreshToken?> GetRefreshTokenAsync(string token)
    {
        return await _context.RefreshTokens
            .Include(x => x.User)
            .FirstOrDefaultAsync(x => x.Token == token);
    }

    public async Task RevokeRefreshTokenAsync(string token)
    {
        var existingToken = await _context.RefreshTokens
            .FirstOrDefaultAsync(x => x.Token == token);

        if (existingToken == null)
        {
            _logger.LogWarning(
                "Attempted to revoke a non-existing refresh token.");

            return;
        }

        if (existingToken.IsRevoked)
        {
            _logger.LogInformation(
                "Refresh token already revoked. TokenId: {TokenId}",
                existingToken.TokenId);

            return;
        }

        existingToken.IsRevoked = true;

        await _context.SaveChangesAsync();

        _logger.LogInformation(
            "Refresh token revoked for User {UserId}",
            existingToken.UserId);
    }

    public async Task ReplaceRefreshTokenAsync(
        RefreshToken existingToken,
        string newToken)
    {
        existingToken.IsRevoked = true;

        var expiryDays = Convert.ToInt32(
            _configuration["Jwt:RefreshTokenExpiryDays"] ?? "7");

        var refreshToken = new RefreshToken
        {
            UserId = existingToken.UserId,
            Token = newToken,
            ExpiryDate = DateTime.UtcNow.AddDays(expiryDays),
            IsRevoked = false,
            CreatedAt = DateTime.UtcNow
        };

        _context.RefreshTokens.Add(refreshToken);

        await _context.SaveChangesAsync();

        _logger.LogInformation(
            "Refresh token rotated for User {UserId}",
            existingToken.UserId);
    }
}