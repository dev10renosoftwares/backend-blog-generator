using BlogGenerator.ServiceModels.v1.Profile;

namespace BlogGenerator.Interfaces.Profile;

public interface IProfileService
{
    Task<UserProfileDto> GetProfileAsync(int userId);

    Task<UserProfileDto> UpdateProfileAsync(int userId,UpdateProfileRequestDto request);

    Task<UploadProfilePictureResponseDto> UploadProfilePictureAsync(int userId,IFormFile file);

    Task DeleteProfilePictureAsync(int userId);

    Task DeleteAccountAsync(int userId, DeleteAccountRequestDto request);
}