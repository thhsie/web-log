using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using MediatR;
using Weblog.Core.SharedKernel;
using Weblog.Query.Application.Blog.Queries;
using Weblog.Query.Data.Repositories.Abstractions;
using Weblog.Query.QueriesModel;

namespace Weblog.Query.Application.Blog.Handlers;

public class GetAllBlogQueryHandler(IBlogReadOnlyRepository repository, ICacheService cacheService)
    : IRequestHandler<GetAllBlogQuery, Result<IEnumerable<BlogQueryModel>>>
{
    private const string CacheKey = nameof(GetAllBlogQuery);

    public async Task<Result<IEnumerable<BlogQueryModel>>> Handle(
          GetAllBlogQuery request,
          CancellationToken cancellationToken)
    {
        // This method will either return the cached data associated with the CacheKey
        // or create it by calling the GetAllAsync method.
        return Result<IEnumerable<BlogQueryModel>>.Success(
            await cacheService.GetOrCreateAsync(CacheKey, repository.GetAllAsync));
    }
}