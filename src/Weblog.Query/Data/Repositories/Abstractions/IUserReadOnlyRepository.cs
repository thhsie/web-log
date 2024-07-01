using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Weblog.Query.Abstractions;
using Weblog.Query.QueriesModel;

namespace Weblog.Query.Data.Repositories.Abstractions;

public interface IUserReadOnlyRepository : IReadOnlyRepository<UserQueryModel, Guid>
{
    Task<IEnumerable<UserQueryModel>> GetAllAsync();
}