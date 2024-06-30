using System;
using System.Threading.Tasks;
using Weblog.Core.SharedKernel;
using Weblog.Domain.ValueObjects;

namespace Weblog.Domain.Entities.UserAggregate;

public interface IUserWriteOnlyRepository : IWriteOnlyRepository<User, Guid>
{
    /// <summary>
    /// Checks if a User with the specified email already exists asynchronously.
    /// </summary>
    /// <param name="email">The email to check.</param>
    /// <returns>True if a User with the email exists, false otherwise.</returns>
    Task<bool> ExistsByEmailAsync(Email email);

    /// <summary>
    /// Checks if a User with the specified email and current ID already exists asynchronously.
    /// </summary>
    /// <param name="email">The email to check.</param>
    /// <param name="currentId">The current ID of the User to exclude from the check.</param>
    /// <returns>True if a User with the email and current ID exists, false otherwise.</returns>
    Task<bool> ExistsByEmailAsync(Email email, Guid currentId);
}