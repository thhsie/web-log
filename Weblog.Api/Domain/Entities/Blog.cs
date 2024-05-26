using System.ComponentModel.DataAnnotations;

namespace Weblog.Api.Domain.Entities;

public class Blog
{
    public int? Id { get; set; }

    [Required]
    [MaxLength(100)]
    [DataType(DataType.Text)]
    public required string Title { get; set; }

    [Required]
    [DataType(DataType.Text)]
    public required string Content { get; set; }

    [MaxLength(100)]
    public string? TruncatedContent { get; set; }
}
