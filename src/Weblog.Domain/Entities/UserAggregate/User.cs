using System;
using Weblog.Core.SharedKernel;
using Weblog.Domain.Entities.UserAggregate.Events;
using Weblog.Domain.ValueObjects;

namespace Weblog.Domain.Entities.UserAggregate;

public class User : BaseEntity, IAggregateRoot
{
    private bool _isDeleted;

    /// <summary>
    /// Initializes a new instance of the User class.
    /// </summary>
    /// <param name="firstName">The first name of the User.</param>
    /// <param name="lastName">The last name of the User.</param>
    /// <param name="gender">The gender of the User.</param>
    /// <param name="email">The email address of the User.</param>
    /// <param name="dateOfBirth">The date of birth of the User.</param>
    public User(string firstName, string lastName, EGender gender, Email email, DateTime dateOfBirth)
    {
        FirstName = firstName;
        LastName = lastName;
        Gender = gender;
        Email = email;
        DateOfBirth = dateOfBirth;

        AddDomainEvent(new UserCreatedEvent(Id, firstName, lastName, gender, email.Address, dateOfBirth));
    }

    /// <summary>
    /// Default constructor for Entity Framework or other ORM frameworks.
    /// </summary>
    public User()
    {
    }

    // Properties
    /// <summary>
    /// Gets the first name of the User.
    /// </summary>
    public string FirstName { get; }

    /// <summary>
    /// Gets the last name of the User.
    /// </summary>
    public string LastName { get; }

    /// <summary>
    /// Gets the gender of the User.
    /// </summary>
    public EGender Gender { get; }

    /// <summary>
    /// Gets or sets the email address of the User.
    /// </summary>
    public Email Email { get; private set; }

    /// <summary>
    /// Gets the date of birth of the User.
    /// </summary>
    public DateTime DateOfBirth { get; }

    /// <summary>
    /// Changes the email address of the User.
    /// </summary>
    /// <param name="newEmail">The new email address.</param>
    public void ChangeEmail(Email newEmail)
    {
        if (Email.Equals(newEmail))
            return;

        Email = newEmail;

        AddDomainEvent(new UserUpdatedEvent(Id, FirstName, LastName, Gender, newEmail.Address, DateOfBirth));
    }

    /// <summary>
    /// Deletes the User.
    /// </summary>
    public void Delete()
    {
        if (_isDeleted) return;

        _isDeleted = true;
        AddDomainEvent(new UserDeletedEvent(Id, FirstName, LastName, Gender, Email.Address, DateOfBirth));
    }
}