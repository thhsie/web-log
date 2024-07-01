using Microsoft.EntityFrameworkCore;
using Weblog.Core.SharedKernel;
using Weblog.Infrastructure.Data.Mappings;

namespace Weblog.Infrastructure.Data.Context;

public class EventStoreDbContext(DbContextOptions<EventStoreDbContext> dbOptions)
    : BaseDbContext<EventStoreDbContext>(dbOptions)
{
    public DbSet<EventStore> EventStores => Set<EventStore>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new EventStoreConfiguration());
    }
}