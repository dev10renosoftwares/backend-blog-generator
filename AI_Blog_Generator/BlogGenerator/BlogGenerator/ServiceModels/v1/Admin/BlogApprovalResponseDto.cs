using BlogGenerator.Enums;

namespace BlogGenerator.ServiceModels.v1
{
    public class BlogApprovalResponseDto
    {
        public int BlogId { get; set; }

        public string Status { get; set; } = string.Empty;

        public string Message { get; set; } = string.Empty;
    }
}