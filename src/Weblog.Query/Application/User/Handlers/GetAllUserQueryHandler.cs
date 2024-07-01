using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using MediatR;
using Weblog.Core.SharedKernel;
using Weblog.Query.Application.User.Queries;
using Weblog.Query.Data.Repositories.Abstractions;
using Weblog.Query.QueriesModel;

namespace Weblog.Query.Application.User.Handlers;

public class GetAllUserQueryHandler(IUserReadOnlyRepository repository, ICacheService cacheService)
    : IRequestHandler<GetAllUserQuery, Result<IEnumerable<UserQueryModel>>>
{
    private const string CacheKey = nameof(GetAllUserQuery);

    public async Task<Result<IEnumerable<UserQueryModel>>> Handle(
          GetAllUserQuery request,
          CancellationToken cancellationToken)
    {
        // This method will either return the cached data associated with the CacheKey
        // or create it by calling the GetAllAsync method.
        return Result<IEnumerable<UserQueryModel>>.Success(
            await cacheService.GetOrCreateAsync(CacheKey, repository.GetAllAsync));
    }
}