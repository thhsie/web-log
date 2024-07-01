using System.Collections.Generic;
using Ardalis.Result;
using MediatR;
using Weblog.Query.QueriesModel;

namespace Weblog.Query.Application.User.Queries;

public class GetAllUserQuery : IRequest<Result<IEnumerable<UserQueryModel>>>;