using BlogGenerator.Enums;

namespace BlogGenerator.ServiceModels.v1

{
    public class AdminBlogApprovalDto
    {
        public int BlogId { get; set; }

        public int UserId { get; set; }

        public string Username { get; set; } = string.Empty;

        public string Title { get; set; } = string.Empty;

        public string? Slug { get; set; }

        public string? Excerpt { get; set; }

        public string? Content { get; set; }

        public int CategoryId { get; set; }

        public string? CategoryName { get; set; }

        public BlogStatus Status { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? PublishedAt { get; set; }
    }
}