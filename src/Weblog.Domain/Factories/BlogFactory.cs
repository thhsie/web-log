using System;
using Ardalis.Result;
using Weblog.Domain.Entities.BlogAggregate;

namespace Weblog.Domain.Factories;

public static class BlogFactory
{
    public static Result<Blog> Create(
        Guid userId,
        string title,
        string content,
        DateTime creationDate)
    {
        return Result<Blog>.Success(new Blog(userId, title, content, creationDate));
    }
}