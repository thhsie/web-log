using Microsoft.EntityFrameworkCore;
using Weblog.Api.Domain.Entities;

namespace Weblog.Api.Infrastructure.Data;

public class BlogDbContext : DbContext
{
    public BlogDbContext(DbContextOptions<BlogDbContext> options) : base(options)
    {
    }

    public DbSet<Blog> Blogs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Blog>().HasData(
            new Blog
            {
                Id = 1,
                Title = "First Blog Post",
                Content = "This is the content of the first blog post.",
                TruncatedContent = "This is the content of the first blog post."
            }
        );
    }
}

