using System;
using Weblog.Core.SharedKernel;
using Weblog.Domain.Entities.BlogAggregate.Events;

namespace Weblog.Domain.Entities.BlogAggregate;

public class Blog : BaseEntity, IAggregateRoot
{
    private bool _isDeleted;

    /// <summary>
    /// Initializes a new instance of the Blog class.
    /// </summary>
    /// <param name="userId">The author's user ID</param>
    /// <param name="title">The title of the Blog.</param>
    /// <param name="content">The content of the Blog.</param>
    /// <param name="creationDate">The date of the Blog.</param>
    public Blog(Guid userId, string title, string content, DateTime creationDate)
    {
        UserId = userId;
        Title = title;
        Content = content;
        LastUpdated = creationDate;

        AddDomainEvent(new BlogCreatedEvent(Id, UserId, Title, Content, LastUpdated));
    }

    /// <summary>
    /// Default constructor for Entity Framework or other ORM frameworks.
    /// </summary>
    public Blog()
    {
    }

    // Properties
    /// <summary>
    /// Gets the author's user identifier.
    /// </summary>
    public Guid UserId { get; }

    /// <summary>
    /// Gets the title of the Blog.
    /// </summary>
    public string Title { get; private set; }

    /// <summary>
    /// Gets the content of the Blog.
    /// </summary>
    public string Content { get; private set; }

    public DateTime LastUpdated { get; private set; }

    /// <summary>
    /// Edits the Blog.
    /// </summary>
    /// <param name="newBlog">The new email address.</param>
    public void Edit(Blog newBlog)
    {
        if (IsBlogUnchanged(newBlog))
            return;

        Title = newBlog.Title;
        Content = newBlog.Content;
        LastUpdated = DateTime.Now;

        AddDomainEvent(new BlogUpdatedEvent(Id, UserId, Title, Content, LastUpdated));
    }

    /// <summary>
    /// Deletes the Blog.
    /// </summary>
    public void Delete()
    {
        if (_isDeleted) return;

        _isDeleted = true;
        AddDomainEvent(new BlogDeletedEvent(Id, UserId, Title, Content, DateTime.Now));
    }

    private bool IsBlogUnchanged(Blog newBlog)
    {
        return Title.Equals(newBlog.Title) && Content.Equals(newBlog.Content);
    }
}