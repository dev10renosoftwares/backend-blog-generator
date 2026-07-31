using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using P14_AI_Blog_Generator_Backend.ApiResponse;
using P14_AI_Blog_Generator_Backend.DTOs.Profile;
using P14_AI_Blog_Generator_Backend.Interfaces;

namespace P14_AI_Blog_Generator_Backend.Controllers;

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
}