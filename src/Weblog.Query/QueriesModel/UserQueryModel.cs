using System;
using Weblog.Query.Abstractions;

namespace Weblog.Query.QueriesModel;

public class UserQueryModel : IQueryModel<Guid>
{
    public UserQueryModel(
        Guid id,
        string firstName,
        string lastName,
        string gender,
        string email,
        DateTime dateOfBirth)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Gender = gender;
        Email = email;
        DateOfBirth = dateOfBirth;
    }

    private UserQueryModel()
    {
    }

    public Guid Id { get; private init; }
    public string FirstName { get; private init; }
    public string LastName { get; private init; }
    public string Gender { get; private init; }
    public string Email { get; private init; }
    public DateTime DateOfBirth { get; private init; }

    public string FullName => (FirstName + " " + LastName).Trim();
}