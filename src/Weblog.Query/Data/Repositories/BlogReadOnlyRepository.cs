using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using Weblog.Query.Abstractions;
using Weblog.Query.Data.Repositories.Abstractions;
using Weblog.Query.QueriesModel;

namespace Weblog.Query.Data.Repositories;

internal class BlogReadOnlyRepository(IReadDbContext readDbContext)
    : BaseReadOnlyRepository<BlogQueryModel, Guid>(readDbContext), IBlogReadOnlyRepository
{
    public async Task<IEnumerable<BlogQueryModel>> GetAllAsync()
    {
        var sort = Builders<BlogQueryModel>.Sort
            .Ascending(Blog => Blog.Title)
            .Descending(Blog => Blog.LastUpdated);

        var findOptions = new FindOptions<BlogQueryModel>
        {
            Sort = sort
        };

        using var asyncCursor = await Collection.FindAsync(Builders<BlogQueryModel>.Filter.Empty, findOptions);
        return await asyncCursor.ToListAsync();
    }
}