using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using FluentValidation;
using MediatR;
using Weblog.Application.User.Commands;
using Weblog.Application.User.Responses;
using Weblog.Core.SharedKernel;
using Weblog.Domain.Entities.UserAggregate;
using Weblog.Domain.Factories;
using Weblog.Domain.ValueObjects;

namespace Weblog.Application.User.Handlers;

public class CreateUserCommandHandler(
    IValidator<CreateUserCommand> validator,
    IUserWriteOnlyRepository repository,
    IUnitOfWork unitOfWork) : IRequestHandler<CreateUserCommand, Result<CreatedUserResponse>>
{
    public async Task<Result<CreatedUserResponse>> Handle(
        CreateUserCommand request,
        CancellationToken cancellationToken)
    {
        // Validating the request.
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            // Return the result with validation errors.
            return Result<CreatedUserResponse>.Invalid(validationResult.AsErrors());
        }

        // Instantiating the Email value object.
        var email = Email.Create(request.Email).Value;

        // Checking if a User with the email address already exists.
        if (await repository.ExistsByEmailAsync(email))
            return Result<CreatedUserResponse>.Error("The provided email address is already in use.");

        // Creating an instance of the User entity.
        // When instantiated, the "UserCreatedEvent" will be created.
        var User = UserFactory.Create(
            request.FirstName,
            request.LastName,
            request.Gender,
            email,
            request.DateOfBirth);

        // Adding the entity to the repository.
        repository.Add(User);

        // Saving changes to the database and triggering events.
        await unitOfWork.SaveChangesAsync();

        // Returning the ID and success message.
        return Result<CreatedUserResponse>.Success(
            new CreatedUserResponse(User.Id), "Successfully registered!");
    }
}