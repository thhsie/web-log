using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using FluentValidation;
using MediatR;
using Weblog.Core.SharedKernel;
using Weblog.Query.Application.User.Queries;
using Weblog.Query.Data.Repositories.Abstractions;
using Weblog.Query.QueriesModel;

namespace Weblog.Query.Application.User.Handlers;

public class GetUserByIdQueryHandler(
    IValidator<GetUserByIdQuery> validator,
    IUserReadOnlyRepository repository,
    ICacheService cacheService) : IRequestHandler<GetUserByIdQuery, Result<UserQueryModel>>
{
    public async Task<Result<UserQueryModel>> Handle(
        GetUserByIdQuery request,
        CancellationToken cancellationToken)
    {
        // Validating the request.
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            // Returns the result with validation errors.
            return Result<UserQueryModel>.Invalid(validationResult.AsErrors());
        }

        // Creating a cache key using the query name and the User ID.
        var cacheKey = $"{nameof(GetUserByIdQuery)}_{request.Id}";

        // Getting the User from the cache service. If not found, fetches it from the repository.
        // The User will be stored in the cache service for future queries.
        var User = await cacheService.GetOrCreateAsync(cacheKey, () => repository.GetByIdAsync(request.Id));

        // If the User is null, returns a result indicating that no User was found.
        // Otherwise, returns a successful result with the User.
        return User == null
            ? Result<UserQueryModel>.NotFound($"No User found by Id: {request.Id}")
            : Result<UserQueryModel>.Success(User);
    }
}