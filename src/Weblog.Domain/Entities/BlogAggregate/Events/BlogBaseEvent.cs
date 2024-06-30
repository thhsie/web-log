using System;

using Weblog.Core.SharedKernel;

namespace Weblog.Domain.Entities.BlogAggregate.Events;

public abstract class BlogBaseEvent : BaseEvent
{
    protected BlogBaseEvent(
        Guid id,
        Guid userId,
        string title,
        string content,
        DateTime lastUpdated)
    {
        Id = id;
        UserId = userId;
        AggregateId = id;
        Title = title;
        Content = content;
        LastUpdated = lastUpdated;
    }

    public Guid Id { get; private init; }
    public Guid UserId { get; private init; }
    public string Title { get; private init; }
    public string Content { get; private init; }
    public DateTime LastUpdated { get; private init; }
}