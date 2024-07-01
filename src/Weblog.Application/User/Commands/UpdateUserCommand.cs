using System;
using System.ComponentModel.DataAnnotations;
using Ardalis.Result;
using MediatR;

namespace Weblog.Application.User.Commands;

public class UpdateUserCommand : IRequest<Result>
{
    [Required]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(200)]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }
}