using System;
using Ardalis.Result;
using MediatR;
using Weblog.Query.QueriesModel;

namespace Weblog.Query.Application.User.Queries;

public class GetUserByIdQuery(Guid id) : IRequest<Result<UserQueryModel>>
{
    public Guid Id { get; } = id;
}