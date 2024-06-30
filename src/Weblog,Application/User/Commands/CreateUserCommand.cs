using System;
using System.ComponentModel.DataAnnotations;
using Ardalis.Result;
using MediatR;
using Weblog.Application.User.Responses;
using Weblog.Domain.Entities.UserAggregate;

namespace Weblog.Application.User.Commands;

public class CreateUserCommand : IRequest<Result<CreatedUserResponse>>
{
    [Required]
    [MaxLength(100)]
    [DataType(DataType.Text)]
    public string FirstName { get; set; }

    [Required]
    [MaxLength(100)]
    [DataType(DataType.Text)]
    public string LastName { get; set; }

    [Required]
    public EGender Gender { get; set; }

    [Required]
    [MaxLength(200)]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public DateTime DateOfBirth { get; set; }
}