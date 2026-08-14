using BlogGenerator.DAL;
using BlogGenerator.Foundation.Exceptions;
using BlogGenerator.Interfaces.Profile;
using BlogGenerator.ServiceModels.v1.Profile;
using Microsoft.EntityFrameworkCore;
using BlogGenerator.DomainModels.v1;
using BlogGenerator.Enums;

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

    public async Task<PublicUserProfileDto> GetPublicProfileAsync(
    int userId,
    int currentUserId)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(x =>
                x.UserId == userId &&
                !x.IsDeleted);

        if (user == null)
        {
            _logger.LogWarning(
                "Public profile not found for UserId {UserId}",
                userId);

            throw new NotFoundException("User not found.");
        }

        var followersCount = await _context.Follows
            .CountAsync(x =>
                x.FollowingUserId == userId);

        var followingCount = await _context.Follows
            .CountAsync(x =>
                x.FollowerUserId == userId);

        var isFollowing = await _context.Follows
            .AnyAsync(x =>
                x.FollowerUserId == currentUserId &&
                x.FollowingUserId == userId);

        return new PublicUserProfileDto
        {
            UserId = user.UserId,
            UserName = user.UserName,
            ProfilePictureUrl = user.ProfilePictureUrl,
            CreatedAt = user.CreatedAt,
            FollowersCount = followersCount,
            FollowingCount = followingCount,
            IsFollowing = isFollowing
        };
    }

    public async Task<List<PublicUserBlogDto>> GetUserBlogsAsync(
        int userId)
    {
        var userExists = await _context.Users
            .AnyAsync(x =>
                x.UserId == userId &&
                !x.IsDeleted);

        if (!userExists)
        {
            throw new NotFoundException("User not found.");
        }

        return await _context.Blogs
            .Where(x =>
                x.UserId == userId &&
                x.Status == BlogStatus.Published &&
                x.PublishedAt.HasValue)
            .OrderByDescending(x => x.PublishedAt)
            .Select(x => new PublicUserBlogDto
            {
                BlogId = x.BlogId,
                Title = x.Title,
                Content = x.Content,
                PublishedAt = x.PublishedAt.Value
            })
            .ToListAsync();
    }

    public async Task<FollowActionResponseDto> FollowUserAsync(
        int currentUserId,
        int userId)
    {
        if (currentUserId == userId)
        {
            throw new BadRequestException(
                "You cannot follow yourself.");
        }

        var user = await _context.Users
            .FirstOrDefaultAsync(x =>
                x.UserId == userId &&
                !x.IsDeleted);

        if (user == null)
        {
            throw new NotFoundException("User not found.");
        }

        var alreadyFollowing = await _context.Follows
            .AnyAsync(x =>
                x.FollowerUserId == currentUserId &&
                x.FollowingUserId == userId);

        if (alreadyFollowing)
        {
            throw new ConflictException(
                "You are already following this user.");
        }

        _context.Follows.Add(new Follow
        {
            FollowerUserId = currentUserId,
            FollowingUserId = userId,
            CreatedAt = DateTime.UtcNow
        });

        await _context.SaveChangesAsync();

        _logger.LogInformation(
            "User {CurrentUserId} followed User {UserId}",
            currentUserId,
            userId);

        return new FollowActionResponseDto
        {
            UserId = userId,
            IsFollowing = true,
            Message = "User followed successfully."
        };
    }

    public async Task<FollowActionResponseDto> UnfollowUserAsync(
        int currentUserId,
        int userId)
    {
        var follow = await _context.Follows
            .FirstOrDefaultAsync(x =>
                x.FollowerUserId == currentUserId &&
                x.FollowingUserId == userId);

        if (follow == null)
        {
            throw new NotFoundException(
                "You are not following this user.");
        }

        _context.Follows.Remove(follow);

        await _context.SaveChangesAsync();

        _logger.LogInformation(
            "User {CurrentUserId} unfollowed User {UserId}",
            currentUserId,
            userId);

        return new FollowActionResponseDto
        {
            UserId = userId,
            IsFollowing = false,
            Message = "User unfollowed successfully."
        };
    }

    public async Task<List<FollowUserDto>> GetFollowersAsync(
        int currentUserId)
    {
        return await _context.Follows
            .Where(x =>
                x.FollowingUserId == currentUserId)
            .Join(
                _context.Users,
                follow => follow.FollowerUserId,
                user => user.UserId,
                (follow, user) => user)
            .Where(user => !user.IsDeleted)
            .Select(user => new FollowUserDto
            {
                UserId = user.UserId,
                UserName = user.UserName,
                ProfilePictureUrl = user.ProfilePictureUrl
            })
            .ToListAsync();
    }

    public async Task<List<FollowUserDto>> GetFollowingAsync(
        int currentUserId)
    {
        return await _context.Follows
            .Where(x =>
                x.FollowerUserId == currentUserId)
            .Join(
                _context.Users,
                follow => follow.FollowingUserId,
                user => user.UserId,
                (follow, user) => user)
            .Where(user => !user.IsDeleted)
            .Select(user => new FollowUserDto
            {
                UserId = user.UserId,
                UserName = user.UserName,
                ProfilePictureUrl = user.ProfilePictureUrl
            })
            .ToListAsync();
    }

    public async Task<List<FollowUserDto>> GetUserFollowersAsync(
        int userId)
    {
        var userExists = await _context.Users
            .AnyAsync(x =>
                x.UserId == userId &&
                !x.IsDeleted);

        if (!userExists)
        {
            throw new NotFoundException("User not found.");
        }

        return await _context.Follows
            .Where(x =>
                x.FollowingUserId == userId)
            .Join(
                _context.Users,
                follow => follow.FollowerUserId,
                user => user.UserId,
                (follow, user) => user)
            .Where(user => !user.IsDeleted)
            .Select(user => new FollowUserDto
            {
                UserId = user.UserId,
                UserName = user.UserName,
                ProfilePictureUrl = user.ProfilePictureUrl
            })
            .ToListAsync();
    }

    public async Task<List<FollowUserDto>> GetUserFollowingAsync(
        int userId)
    {
        var userExists = await _context.Users
            .AnyAsync(x =>
                x.UserId == userId &&
                !x.IsDeleted);

        if (!userExists)
        {
            throw new NotFoundException("User not found.");
        }

        return await _context.Follows
            .Where(x =>
                x.FollowerUserId == userId)
            .Join(
                _context.Users,
                follow => follow.FollowingUserId,
                user => user.UserId,
                (follow, user) => user)
            .Where(user => !user.IsDeleted)
            .Select(user => new FollowUserDto
            {
                UserId = user.UserId,
                UserName = user.UserName,
                ProfilePictureUrl = user.ProfilePictureUrl
            })
            .ToListAsync();
    }
}