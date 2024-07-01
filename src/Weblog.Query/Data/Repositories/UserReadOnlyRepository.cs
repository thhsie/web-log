using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using Weblog.Query.Abstractions;
using Weblog.Query.Data.Repositories.Abstractions;
using Weblog.Query.QueriesModel;

namespace Weblog.Query.Data.Repositories;

internal class UserReadOnlyRepository(IReadDbContext readDbContext)
    : BaseReadOnlyRepository<UserQueryModel, Guid>(readDbContext), IUserReadOnlyRepository
{
    public async Task<IEnumerable<UserQueryModel>> GetAllAsync()
    {
        var sort = Builders<UserQueryModel>.Sort
            .Ascending(User => User.FirstName)
            .Descending(User => User.DateOfBirth);

        var findOptions = new FindOptions<UserQueryModel>
        {
            Sort = sort
        };

        using var asyncCursor = await Collection.FindAsync(Builders<UserQueryModel>.Filter.Empty, findOptions);
        return await asyncCursor.ToListAsync();
    }
}