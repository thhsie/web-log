using System;
using Ardalis.Result;
using MediatR;

namespace Weblog.Application.User.Commands;

public class DeleteUserCommand(Guid id) : IRequest<Result>
{
    public Guid Id { get; } = id;
}