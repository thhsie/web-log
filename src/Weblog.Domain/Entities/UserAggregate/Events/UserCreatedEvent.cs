using System;

namespace Weblog.Domain.Entities.UserAggregate.Events;

public class UserCreatedEvent(
    Guid id,
    string firstName,
    string lastName,
    EGender gender,
    string email,
    DateTime dateOfBirth) : UserBaseEvent(id, firstName, lastName, gender, email, dateOfBirth);