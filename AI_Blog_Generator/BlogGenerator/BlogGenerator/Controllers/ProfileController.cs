using BlogGenerator.Interfaces.Profile;
using BlogGenerator.ServiceModels.v1.Foundation;
using BlogGenerator.ServiceModels.v1.Profile;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BlogGenerator.Controllers;

[ApiController]
[Route("api/profile")]
[Authorize]
public class ProfileController : ControllerBase
{
    private readonly IProfileService _profileService;

    public ProfileController(IProfileService profileService)
    {
        _profileService = profileService;
    }

    private int UserId =>
        int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    [HttpGet]
    public async Task<IActionResult> GetProfile()
    {
        var result = await _profileService.GetProfileAsync(UserId);

        return Ok(new ApiResponse<UserProfileDto>
        {
            Success = true,
            Message = "Profile retrieved successfully.",
            Data = result
        });
    }

    [HttpPut]
    public async Task<IActionResult> UpdateProfile(
        UpdateProfileRequestDto request)
    {
        var result = await _profileService.UpdateProfileAsync(
            UserId,
            request);

        return Ok(new ApiResponse<UserProfileDto>
        {
            Success = true,
            Message = "Profile updated successfully.",
            Data = result
        });
    }

    [HttpPost("upload-picture")]
    public async Task<IActionResult> UploadPicture(
        IFormFile file)
    {
        var result = await _profileService.UploadProfilePictureAsync(
            UserId,
            file);

        return Ok(new ApiResponse<UploadProfilePictureResponseDto>
        {
            Success = true,
            Message = "Profile picture uploaded successfully.",
            Data = result
        });
    }

    [HttpDelete("picture")]
    public async Task<IActionResult> DeletePicture()
    {
        await _profileService.DeleteProfilePictureAsync(UserId);

        return Ok(new ApiResponse<object>
        {
            Success = true,
            Message = "Profile picture deleted successfully."
        });
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteAccount(
        DeleteAccountRequestDto request)
    {
        await _profileService.DeleteAccountAsync(
            UserId,
            request);

        return Ok(new ApiResponse<object>
        {
            Success = true,
            Message = "Account deleted successfully."
        });
    }

    [HttpPut("change-password")]
    public async Task<IActionResult> ChangePassword(
    [FromBody] ChangePasswordRequestDto request)
    {
        var userId = UserId;

        await _profileService.ChangePasswordAsync(userId, request);

        return Ok(new { message = "Password changed successfully." });
    }

    [HttpGet("{userId}")]
    [Authorize]
    public async Task<IActionResult> GetPublicProfile(int userId)
    {
        var currentUserId = int.Parse(
            User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        var result = await _profileService
            .GetPublicProfileAsync(userId, currentUserId);

        return Ok(result);
    }

    [HttpGet("{userId}/blogs")]
    [Authorize]
    public async Task<IActionResult> GetUserBlogs(int userId)
    {
        var result = await _profileService
            .GetUserBlogsAsync(userId);

        return Ok(result);
    }

    [HttpPost("{userId}/follow")]
    [Authorize]
    public async Task<IActionResult> FollowUser(int userId)
    {
        var currentUserId = int.Parse(
            User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        var result = await _profileService
            .FollowUserAsync(currentUserId, userId);

        return Ok(result);
    }

    [HttpDelete("{userId}/follow")]
    [Authorize]
    public async Task<IActionResult> UnfollowUser(int userId)
    {
        var currentUserId = int.Parse(
            User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        var result = await _profileService
            .UnfollowUserAsync(currentUserId, userId);

        return Ok(result);
    }

    [HttpGet("followers")]
    [Authorize]
    public async Task<IActionResult> GetFollowers()
    {
        var currentUserId = int.Parse(
            User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        var result = await _profileService
            .GetFollowersAsync(currentUserId);

        return Ok(result);
    }

    [HttpGet("following")]
    [Authorize]
    public async Task<IActionResult> GetFollowing()
    {
        var currentUserId = int.Parse(
            User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        var result = await _profileService
            .GetFollowingAsync(currentUserId);

        return Ok(result);
    }

    [HttpGet("{userId}/followers")]
    [Authorize]
    public async Task<IActionResult> GetUserFollowers(int userId)
    {
        var result = await _profileService
            .GetUserFollowersAsync(userId);

        return Ok(result);
    }

    [HttpGet("{userId}/following")]
    [Authorize]
    public async Task<IActionResult> GetUserFollowing(int userId)
    {
        var result = await _profileService
            .GetUserFollowingAsync(userId);

        return Ok(result);
    }
}