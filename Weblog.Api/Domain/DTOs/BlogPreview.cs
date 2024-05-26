using System.ComponentModel.DataAnnotations;

namespace Weblog.Api.Domain.DTOs;
public class BlogPreview
{
    [Required]
    public int? Id { get; set; }
    
    [Required]
    [DataType(DataType.Text)]
    public required string Title { get; set; }

    [Required]
    [MaxLength(100)]
    [DataType(DataType.Text)]
    public required string TruncatedContent { get; set; }
}
