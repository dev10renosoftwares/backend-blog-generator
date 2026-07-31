using P14_AI_Blog_Generator_Backend.DTOs.Profile;

namespace P14_AI_Blog_Generator_Backend.Interfaces;

public interface IProfileService
{
    Task<UserProfileDto> GetProfileAsync(int userId);

    Task<UserProfileDto> UpdateProfileAsync(int userId,UpdateProfileRequestDto request);

    Task<UploadProfilePictureResponseDto> UploadProfilePictureAsync(int userId,IFormFile file);

    Task DeleteProfilePictureAsync(int userId);

    Task DeleteAccountAsync(int userId, DeleteAccountRequestDto request);
}