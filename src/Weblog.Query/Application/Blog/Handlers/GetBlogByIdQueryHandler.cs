using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using FluentValidation;
using MediatR;
using Weblog.Core.SharedKernel;
using Weblog.Query.Application.Blog.Queries;
using Weblog.Query.Data.Repositories.Abstractions;
using Weblog.Query.QueriesModel;

namespace Weblog.Query.Application.Blog.Handlers;

public class GetBlogByIdQueryHandler(
    IValidator<GetBlogByIdQuery> validator,
    IBlogReadOnlyRepository  repository,
    ICacheService cacheService) : IRequestHandler<GetBlogByIdQuery, Result<BlogQueryModel>>
{
    public async Task<Result<BlogQueryModel>> Handle(
        GetBlogByIdQuery request,
        CancellationToken cancellationToken)
    {
        // Validating the request.
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            // Returns the result with validation errors.
            return Result<BlogQueryModel>.Invalid(validationResult.AsErrors());
        }

        // Creating a cache key using the query name and the Blog ID.
        var cacheKey = $"{nameof(GetBlogByIdQuery)}_{request.Id}";

        // Getting the Blog from the cache service. If not found, fetches it from the repository.
        // The Blog will be stored in the cache service for future queries.
        var Blog = await cacheService.GetOrCreateAsync(cacheKey, () => repository.GetByIdAsync(request.Id));

        // If the Blog is null, returns a result indicating that no Blog was found.
        // Otherwise, returns a successful result with the Blog.
        return Blog == null
            ? Result<BlogQueryModel>.NotFound($"No Blog found by Id: {request.Id}")
            : Result<BlogQueryModel>.Success(Blog);
    }
}