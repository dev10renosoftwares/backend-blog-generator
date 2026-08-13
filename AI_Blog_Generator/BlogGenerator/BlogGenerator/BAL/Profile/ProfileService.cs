using BlogGenerator.DAL;
using BlogGenerator.Foundation.Exceptions;
using BlogGenerator.Interfaces.Profile;
using BlogGenerator.ServiceModels.v1.Profile;
using Microsoft.EntityFrameworkCore;

namespace BlogGenerator.BAL.Profile;

public class ProfileService : IProfileService
{
    private readonly ApplicationDbContext _context;

    private readonly ILogger<ProfileService> _logger;

    public ProfileService(
        ApplicationDbContext context,
        ILogger<ProfileService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<UserProfileDto> GetProfileAsync(int userId)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(x => x.UserId == userId && !x.IsDeleted);

        if (user == null)
        {
            _logger.LogWarning(
                "Profile not found for UserId {UserId}",
                userId);

            throw new NotFoundException("User not found.");
        }

        _logger.LogInformation(
            "Profile retrieved for UserId {UserId}",
            userId);

        return new UserProfileDto
        {
            UserId = user.UserId,
            UserName = user.UserName,
            Email = user.Email,
            ProfilePictureUrl = user.ProfilePictureUrl,
            AvailableCredits = user.AvailableCredits,
            Role = user.Role.ToString(),
            CreatedAt = user.CreatedAt
        };
    }

    public async Task<UserProfileDto> UpdateProfileAsync(
    int userId,
    UpdateProfileRequestDto request)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(x => x.UserId == userId && !x.IsDeleted);

        if (user == null)
        {
            _logger.LogWarning(
                "Profile update failed. User {UserId} not found.",
                userId);

            throw new NotFoundException("User not found.");
        }

        var userNameExists = await _context.Users
            .AnyAsync(x =>
                x.UserName == request.UserName &&
                x.UserId != userId);

        if (userNameExists)
        {
            _logger.LogWarning(
                "Username {UserName} already exists.",
                request.UserName);

            throw new ConflictException("Username already exists.");
        }

        user.UserName = request.UserName;
        user.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        _logger.LogInformation(
            "Profile updated for UserId {UserId}",
            userId);

        return new UserProfileDto
        {
            UserId = user.UserId,
            UserName = user.UserName,
            Email = user.Email,
            ProfilePictureUrl = user.ProfilePictureUrl,
            AvailableCredits = user.AvailableCredits,
            Role = user.Role.ToString(),
            CreatedAt = user.CreatedAt
        };
    }

    public async Task<UploadProfilePictureResponseDto> UploadProfilePictureAsync(
    int userId,
    IFormFile file)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(x => x.UserId == userId && !x.IsDeleted);

        if (user == null)
        {
            throw new NotFoundException("User not found.");
        }

        if (file == null || file.Length == 0)
        {
            throw new BadRequestException("Please select a valid image.");
        }

        var uploadsFolder = Path.Combine(
            Directory.GetCurrentDirectory(),
            "wwwroot",
            "ProfilePictures");

        if (!Directory.Exists(uploadsFolder))
        {
            Directory.CreateDirectory(uploadsFolder);
        }

        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";

        var filePath = Path.Combine(uploadsFolder, fileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        user.ProfilePictureUrl = $"/ProfilePictures/{fileName}";
        user.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        _logger.LogInformation(
            "Profile picture uploaded for UserId {UserId}",
            userId);

        return new UploadProfilePictureResponseDto
        {
            ProfilePictureUrl = user.ProfilePictureUrl
        };
    }

    public async Task DeleteProfilePictureAsync(int userId)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(x => x.UserId == userId && !x.IsDeleted);

        if (user == null)
        {
            throw new NotFoundException("User not found.");
        }

        if (string.IsNullOrWhiteSpace(user.ProfilePictureUrl))
        {
            throw new BadRequestException("Profile picture does not exist.");
        }

        var filePath = Path.Combine(
            Directory.GetCurrentDirectory(),
            "wwwroot",
            user.ProfilePictureUrl.TrimStart('/').Replace("/", Path.DirectorySeparatorChar.ToString()));

        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }

        user.ProfilePictureUrl = null;
        user.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        _logger.LogInformation(
            "Profile picture deleted for UserId {UserId}",
            userId);
    }

    public async Task DeleteAccountAsync(
    int userId,
    DeleteAccountRequestDto request)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(x => x.UserId == userId);

        if (user == null)
        {
            throw new NotFoundException("User not found.");
        }

        if (user.IsDeleted)
        {
            throw new BadRequestException("Account is already deleted.");
        }

        user.IsDeleted = true;
        user.UpdatedAt = DateTime.UtcNow;

        //_context.DeletedAccounts.Add(new DeletedAccount
        //{
        //    UserId = user.UserId,
        //    Email = user.Email,
        //    Reason = request.Reason,
        //    DeletedAt = DateTime.UtcNow
        //});

        await _context.SaveChangesAsync();

        _logger.LogInformation(
            "User {UserId} deleted the account.",
            userId);
    }

    public async Task ChangePasswordAsync(
    int userId,
    ChangePasswordRequestDto request)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.UserId == userId);

        if (user == null)
            throw new KeyNotFoundException("User not found.");

        if (!BCrypt.Net.BCrypt.Verify(request.CurrentPassword, user.PasswordHash))
            throw new UnauthorizedAccessException("Current password is incorrect.");

        if (request.NewPassword != request.ConfirmNewPassword)
            throw new ArgumentException("New password and confirm password do not match.");

        if (BCrypt.Net.BCrypt.Verify(request.NewPassword, user.PasswordHash))
            throw new ArgumentException("New password must be different from the current password.");

        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);

        await _context.SaveChangesAsync();
    }
}