using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using FluentValidation;
using MediatR;
using Weblog.Application.User.Commands;
using Weblog.Core.SharedKernel;
using Weblog.Domain.Entities.UserAggregate;
using Weblog.Domain.ValueObjects;

namespace Weblog.Application.User.Handlers;

public class UpdateUserCommandHandler(
    IValidator<UpdateUserCommand> validator,
    IUserWriteOnlyRepository repository,
    IUnitOfWork unitOfWork) : IRequestHandler<UpdateUserCommand, Result>
{
    public async Task<Result> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        // Validating the request.
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            // Returns the result with validation errors.
            return Result.Invalid(validationResult.AsErrors());
        }

        // Getting the User from the database.
        var User = await repository.GetByIdAsync(request.Id);
        if (User == null)
            return Result.NotFound($"No User found by Id: {request.Id}");

        // Instantiating the Email value object.
        var emailResult = Email.Create(request.Email);
        if (!emailResult.IsSuccess)
            return Result.Error(new ErrorList(emailResult.Errors.ToArray()));

        // Checking if there is already a User with the email address.
        if (await repository.ExistsByEmailAsync(emailResult.Value, User.Id))
            return Result.Error("The provided email address is already in use.");

        // Changing the email in the entity.
        User.ChangeEmail(emailResult.Value);

        // Updating the entity in the repository.
        repository.Update(User);

        // Saving the changes to the database and firing events.
        await unitOfWork.SaveChangesAsync();

        // Returning the success message.
        return Result.SuccessWithMessage("Updated successfully!");
    }
}