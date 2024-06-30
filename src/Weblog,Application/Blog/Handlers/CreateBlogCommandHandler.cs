using System;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using FluentValidation;
using MediatR;
using Weblog.Application.Blog.Commands;
using Weblog.Application.Blog.Responses;
using Weblog.Core.SharedKernel;
using Weblog.Domain.Factories;

namespace Weblog.Application.Blog.Handlers;

public class CreateBlogCommandHandler(
    IValidator<CreateBlogCommand> validator,
    IWriteOnlyRepository<Domain.Entities.BlogAggregate.Blog, Guid> repository,
    IUnitOfWork unitOfWork) : IRequestHandler<CreateBlogCommand, Result<CreatedBlogResponse>>
{
    public async Task<Result<CreatedBlogResponse>> Handle(
        CreateBlogCommand request,
        CancellationToken cancellationToken)
    {
        // Validating the request.
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            // Return the result with validation errors.
            return Result<CreatedBlogResponse>.Invalid(validationResult.AsErrors());
        }

        // Creating an instance of the Blog entity.
        // When instantiated, the "BlogCreatedEvent" will be created.
        var Blog = BlogFactory.Create(
            request.UserId,
            request.Title,
            request.Content,
            DateTime.Now);

        // Adding the entity to the repository.
        repository.Add(Blog);

        // Saving changes to the database and triggering events.
        await unitOfWork.SaveChangesAsync();

        // Returning the ID and success message.
        return Result<CreatedBlogResponse>.Success(
            new CreatedBlogResponse(Blog.Value.Id), "Successfully registered!");
    }
}