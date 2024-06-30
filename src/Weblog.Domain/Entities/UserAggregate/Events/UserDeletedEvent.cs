using System;

namespace Weblog.Domain.Entities.UserAggregate.Events;

public class UserDeletedEvent(
    Guid id,
    string firstName,
    string lastName,
    EGender gender,
    string email,
    DateTime dateOfBirth) : UserBaseEvent(id, firstName, lastName, gender, email, dateOfBirth);