using System;
using System.ComponentModel.DataAnnotations;
using Ardalis.Result;
using MediatR;
using Weblog.Application.Blog.Responses;

namespace Weblog.Application.Blog.Commands;

public class CreateBlogCommand : IRequest<Result<CreatedBlogResponse>>
{
    [Required]
    [DataType(DataType.Text)]
    public string Title { get; set; }

    [Required]
    [DataType(DataType.Text)]
    public string Content { get; set; }

    [Required]
    public Guid UserId { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public DateTime LastUpdated { get; set; }
}