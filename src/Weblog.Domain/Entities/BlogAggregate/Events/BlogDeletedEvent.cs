using System;

namespace Weblog.Domain.Entities.BlogAggregate.Events;

public class BlogDeletedEvent(
        Guid id,
        Guid userId,
        string title,
        string content,
        DateTime lastUpdated) : BlogBaseEvent(id, userId, title, content, lastUpdated);