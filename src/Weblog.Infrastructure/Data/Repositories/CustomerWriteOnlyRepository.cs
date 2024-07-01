using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Weblog.Domain.Entities.UserAggregate;
using Weblog.Domain.ValueObjects;
using Weblog.Infrastructure.Data.Context;
using Weblog.Infrastructure.Data.Repositories.Common;

namespace Weblog.Infrastructure.Data.Repositories;

internal class UserWriteOnlyRepository(WriteDbContext context)
    : BaseWriteOnlyRepository<User, Guid>(context), IUserWriteOnlyRepository
{
    public async Task<bool> ExistsByEmailAsync(Email email) =>
        await Context.Users
            .AsNoTracking()
            .AnyAsync(user => user.Email.Address == email.Address);

    public async Task<bool> ExistsByEmailAsync(Email email, Guid currentId) =>
        await Context.Users
            .AsNoTracking()
            .AnyAsync(user => user.Email.Address == email.Address && user.Id != currentId);
}