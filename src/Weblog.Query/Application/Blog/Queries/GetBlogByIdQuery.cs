using System;
using Ardalis.Result;
using MediatR;
using Weblog.Query.QueriesModel;

namespace Weblog.Query.Application.Blog.Queries;

public class GetBlogByIdQuery(Guid id) : IRequest<Result<BlogQueryModel>>
{
    public Guid Id { get; } = id;
}