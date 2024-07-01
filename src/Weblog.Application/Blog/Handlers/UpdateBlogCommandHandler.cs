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

public class UpdateBlogCommandHandler(
    IValidator<UpdateBlogCommand> validator,
    IWriteOnlyRepository<Domain.Entities.BlogAggregate.Blog, Guid> repository,
    IUnitOfWork unitOfWork) : IRequestHandler<UpdateBlogCommand, Result>
{
    public async Task<Result> Handle(UpdateBlogCommand request, CancellationToken cancellationToken)
    {
        // Validating the request.
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            // Returns the result with validation errors.
            return Result.Invalid(validationResult.AsErrors());
        }

        // Getting the Blog from the database.
        var Blog = await repository.GetByIdAsync(request.Id);
        if (Blog is null)
            return Result.NotFound($"No Blog found by Id: {request.Id}");

        // Changing the email in the entity.
        Blog.Edit(new Domain.Entities.BlogAggregate.Blog(request.Id, request.Title, request.Content, DateTime.Now));

        // Updating the entity in the repository.
        repository.Update(Blog);

        // Saving the changes to the database and firing events.
        await unitOfWork.SaveChangesAsync();

        // Returning the success message.
        return Result.SuccessWithMessage("Updated successfully!");
    }
}