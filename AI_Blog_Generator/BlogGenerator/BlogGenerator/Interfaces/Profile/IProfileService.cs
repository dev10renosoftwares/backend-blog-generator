using BlogGenerator.ServiceModels.v1.Profile;

namespace BlogGenerator.Interfaces.Profile;

public interface IProfileService
{
    Task<UserProfileDto> GetProfileAsync(int userId);

    Task<UserProfileDto> UpdateProfileAsync(int userId,UpdateProfileRequestDto request);

    Task<UploadProfilePictureResponseDto> UploadProfilePictureAsync(int userId,IFormFile file);

    Task DeleteProfilePictureAsync(int userId);

    Task DeleteAccountAsync(int userId, DeleteAccountRequestDto request);
    Task ChangePasswordAsync(int userId, ChangePasswordRequestDto request);

    // Public profile
    Task<PublicUserProfileDto> GetPublicProfileAsync(int userId,int currentUserId);

    Task<List<PublicUserBlogDto>> GetUserBlogsAsync( int userId);

    // Follow / Unfollow
    Task<FollowActionResponseDto> FollowUserAsync(int currentUserId,int userId);

    Task<FollowActionResponseDto> UnfollowUserAsync( int currentUserId,int userId);

    // Current user's followers / following
    Task<List<FollowUserDto>> GetFollowersAsync( int currentUserId);

    Task<List<FollowUserDto>> GetFollowingAsync(int currentUserId);

    // Another user's followers / following
    Task<List<FollowUserDto>> GetUserFollowersAsync( int userId);

    Task<List<FollowUserDto>> GetUserFollowingAsync(int userId);

    Task<List<SavedBlogDto>> GetSavedBlogsAsync(int userId);

    Task<List<LikedBlogDto>> GetLikedBlogsAsync(int userId);

    Task<UserStatsDto> GetUserStatsAsync(int userId);

    Task<List<UserActivityDto>> GetActivityAsync(int userId);

    Task<List<UserSearchDto>> SearchUsersAsync(string searchTerm,int currentUserId);

    Task<List<UserSuggestionDto>> GetUserSuggestionsAsync(int currentUserId);

    Task<List<UserBadgeDto>> GetUserBadgesAsync(int userId);
}