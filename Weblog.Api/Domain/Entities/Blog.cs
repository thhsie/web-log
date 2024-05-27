namespace Weblog.Api.Domain.Entities;

public record Blog
{
    public int? Id { get; init; }
    public required string Title { get; set; }
    public required string Content { get; set; }
    public string? TruncatedContent { get; set; }
}
