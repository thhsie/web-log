using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Weblog.Query.Abstractions;
using Weblog.Query.QueriesModel;

namespace Weblog.Query.Data.Repositories.Abstractions;

public interface IBlogReadOnlyRepository : IReadOnlyRepository<BlogQueryModel, Guid>
{
    Task<IEnumerable<BlogQueryModel>> GetAllAsync();
}