using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using FluentValidation;
using MediatR;
using Weblog.Application.User.Commands;
using Weblog.Core.SharedKernel;
using Weblog.Domain.Entities.UserAggregate;

namespace Weblog.Application.User.Handlers;

public class DeleteUserCommandHandler(
    IValidator<DeleteUserCommand> validator,
    IUserWriteOnlyRepository repository,
    IUnitOfWork unitOfWork) : IRequestHandler<DeleteUserCommand, Result>
{
    public async Task<Result> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        // Validating the request.
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            // Returns the result with validation errors.
            return Result.Invalid(validationResult.AsErrors());
        }

        // Retrieving the User from the database.
        var User = await repository.GetByIdAsync(request.Id);
        if (User is null)
            return Result.NotFound($"No User found by Id: {request.Id}");

        // Marking the entity as deleted, the UserDeletedEvent will be added.
        User.Delete();

        // Removing the entity from the repository.
        repository.Remove(User);

        // Saving the changes to the database and triggering the events.
        await unitOfWork.SaveChangesAsync();

        // Returning the success message.
        return Result.SuccessWithMessage("Successfully removed!");
    }
}