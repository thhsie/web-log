using System;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using FluentValidation;
using MediatR;
using Weblog.Application.Blog.Commands;
using Weblog.Core.SharedKernel;

namespace Weblog.Application.Blog.Handlers;

public class DeleteBlogCommandHandler(
    IValidator<DeleteBlogCommand> validator,
    IWriteOnlyRepository<Domain.Entities.BlogAggregate.Blog, Guid> repository,
    IUnitOfWork unitOfWork) : IRequestHandler<DeleteBlogCommand, Result>
{
    public async Task<Result> Handle(DeleteBlogCommand request, CancellationToken cancellationToken)
    {
        // Validating the request.
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            // Returns the result with validation errors.
            return Result.Invalid(validationResult.AsErrors());
        }

        // Retrieving the Blog from the database.
        var Blog = await repository.GetByIdAsync(request.Id);
        if (Blog == null)
            return Result.NotFound($"No Blog found by Id: {request.Id}");

        // Marking the entity as deleted, the BlogDeletedEvent will be added.
        Blog.Delete();

        // Removing the entity from the repository.
        repository.Remove(Blog);

        // Saving the changes to the database and triggering the events.
        await unitOfWork.SaveChangesAsync();

        // Returning the success message.
        return Result.SuccessWithMessage("Successfully removed!");
    }
}