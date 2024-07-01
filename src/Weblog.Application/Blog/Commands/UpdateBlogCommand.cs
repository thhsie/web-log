using System;
using System.ComponentModel.DataAnnotations;
using Ardalis.Result;
using MediatR;

namespace Weblog.Application.Blog.Commands;

public class UpdateBlogCommand : IRequest<Result>
{
    [Required]
    public Guid Id { get; set; }

    [Required]
    [DataType(DataType.Text)]
    public string Title { get; set; }

    [Required]
    [DataType(DataType.Text)]
    public string Content { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public DateTime LastUpdated { get; set; }
}