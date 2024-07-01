using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.DataEncryption;
using Microsoft.EntityFrameworkCore.DataEncryption.Providers;
using Weblog.Domain.Entities.UserAggregate;
using Weblog.Domain.Entities.BlogAggregate;
using Weblog.Infrastructure.Data.Mappings;

namespace Weblog.Infrastructure.Data.Context;

public class WriteDbContext(DbContextOptions<WriteDbContext> dbOptions)
    : BaseDbContext<WriteDbContext>(dbOptions)
{
    #region Encryption

    private static readonly byte[] EncryptionKey = [189, 3, 80, 118, 242, 164, 9, 197, 106, 166, 122, 118, 161, 212, 106, 26, 171, 18, 178, 98, 86, 102, 196, 6, 78, 249, 4, 164, 66, 154, 218, 126];
    private static readonly byte[] EncryptionVector = [163, 225, 4, 33, 227, 178, 113, 128, 174, 23, 9, 144, 213, 158, 134, 48];

    private readonly IEncryptionProvider _encryptionProvider = new AesProvider(EncryptionKey, EncryptionVector);

    #endregion

    public DbSet<Blog> Blogs => Set<Blog>();

    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.UseEncryption(_encryptionProvider);
    }
}