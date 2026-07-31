

using P14_AI_Blog_Generator_Backend.DTOs.Authentication;

namespace P14_AI_Blog_Generator_Backend.Interfaces;

public interface IAuthService
{
    Task<AuthResponseDto> RegisterAsync(RegisterRequestDto request);

    Task<AuthResponseDto> LoginAsync(LoginRequestDto request);

    Task<AuthResponseDto> RefreshTokenAsync(RefreshTokenRequestDto request);

    Task LogoutAsync(LogoutRequestDto request);
}