using System;
using Ardalis.Result;
using MediatR;

namespace Weblog.Application.Blog.Commands;

public class DeleteBlogCommand(Guid id) : IRequest<Result>
{
    public Guid Id { get; } = id;
}