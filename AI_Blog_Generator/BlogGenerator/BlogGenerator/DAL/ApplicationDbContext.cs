using BlogGenerator.DomainModels.v1;
using Microsoft.EntityFrameworkCore;

namespace BlogGenerator.DAL
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options)
        {
        }
        public DbSet<User> Users => Set<User>();

        public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();

        public DbSet<Blog> Blogs => Set<Blog>();

        public DbSet<BlogVersion> BlogVersions => Set<BlogVersion>();

        public DbSet<BlogImage> BlogImages => Set<BlogImage>();

        public DbSet<Plan> Plans => Set<Plan>();

        public DbSet<Payment> Payments => Set<Payment>();

        public DbSet<Feedback> Feedbacks => Set<Feedback>();

        public DbSet<Issue> Issues => Set<Issue>();

        public DbSet<DeletedAccount> DeletedAccounts => Set<DeletedAccount>();
    }
}
