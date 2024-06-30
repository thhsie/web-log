using System;
using System.Linq;
using Ardalis.Result;
using Weblog.Domain.Entities.UserAggregate;
using Weblog.Domain.ValueObjects;

namespace Weblog.Domain.Factories;

public static class UserFactory
{
    public static Result<User> Create(
        string firstName,
        string lastName,
        EGender gender,
        string email,
        DateTime dateOfBirth)
    {
        var emailResult = Email.Create(email);
        return !emailResult.IsSuccess
            ? Result<User>.Error(new ErrorList(emailResult.Errors.ToArray()))
            : Result<User>.Success(new User(firstName, lastName, gender, emailResult.Value, dateOfBirth));
    }

    public static User Create(string firstName, string lastName, EGender gender, Email email, DateTime dateOfBirth)
        => new(firstName, lastName, gender, email, dateOfBirth);
}