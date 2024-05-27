namespace Weblog.Api.Domain.Entities;

public record Blog(int? Id, string Title, string Content, string? TruncatedContent);
