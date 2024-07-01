using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Weblog.Domain.Entities.BlogAggregate;
using Weblog.Infrastructure.Data.Extensions;

namespace Weblog.Infrastructure.Data.Mappings;

internal class BlogConfiguration : IEntityTypeConfiguration<Blog>
{
    public void Configure(EntityTypeBuilder<Blog> builder)
    {
        builder
            .ConfigureBaseEntity();

        builder
            .Property(blog => blog.Title)
            .IsRequired(); // NOT NULL

        builder
            .Property(blog => blog.Content)
            .IsRequired(); // NOT NULL

        builder
            .Property(blog => blog.UserId)
            .IsRequired(); // NOT NULL

        builder
            .Property(blog => blog.LastUpdated)
            .IsRequired() // NOT NULL
            .HasColumnType("DATE");
    }
}