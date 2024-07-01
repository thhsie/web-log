using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.DataEncryption;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Weblog.Domain.Entities.UserAggregate;
using Weblog.Infrastructure.Data.Extensions;

namespace Weblog.Infrastructure.Data.Mappings;

internal class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder
            .ConfigureBaseEntity();

        builder
            .Property(user => user.FirstName)
            .IsRequired() // NOT NULL
            .HasMaxLength(100);

        builder
            .Property(user => user.LastName)
            .IsRequired() // NOT NULL
            .HasMaxLength(100);

        builder
            .Property(user => user.Gender)
            .IsRequired() // NOT NULL
            .HasMaxLength(6)
            .HasConversion<string>();

        // Value Object Mapping (ValueObject)
        builder.OwnsOne(user => user.Email, ownedNav =>
        {
            ownedNav
                .Property(email => email.Address)
                .IsRequired() // NOT NULL
                .HasMaxLength(254)
                .HasColumnName(nameof(User.Email))
                .IsEncrypted(); // Encryption

            // Unique Index
            ownedNav
                .HasIndex(email => email.Address)
                .IsUnique();
        });

        builder
            .Property(user => user.DateOfBirth)
            .IsRequired() // NOT NULL
            .HasColumnType("DATE");
    }
}