namespace Weblog.Api.Domain.Entities;

public class Blog
{
    public int? Id { get; set; }
    public required string Title { get; set; }
    public required string Content { get; set; }
    public string? TruncatedContent { get; set; }
}
