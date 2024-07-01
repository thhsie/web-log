using System;
using Weblog.Query.Abstractions;

namespace Weblog.Query.QueriesModel;

public class BlogQueryModel : IQueryModel<Guid>
{
    public BlogQueryModel(
        Guid id,
        Guid userId,
        string title,
        string content,
        DateTime lastUpdated)
    {
        Id = id;
        UserId = userId;
        Title = title;
        Content = content;
        LastUpdated = lastUpdated;
    }

    private BlogQueryModel()
    {
    }

    public Guid Id { get; private init; }
    public Guid UserId { get; private init; }
    public string Title { get; private init; }
    public string Content { get; private init; }
    public DateTime LastUpdated { get; private init; }

    public string TruncatedContent => Content.Substring(0, Math.Min(100, Content.Length)) + "...";
}