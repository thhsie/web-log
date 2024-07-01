using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Weblog.Core.Extensions;
using Weblog.Core.SharedKernel;
using Weblog.Domain.Entities.BlogAggregate.Events;
using Weblog.Query.Abstractions;
using Weblog.Query.Application.Blog.Queries;
using Weblog.Query.QueriesModel;

namespace Weblog.Query.EventHandlers;

public class BlogEventHandler(
    IMapper mapper,
    ISynchronizeDb synchronizeDb,
    ICacheService cacheService,
    ILogger<BlogEventHandler> logger) :
    INotificationHandler<BlogCreatedEvent>,
    INotificationHandler<BlogUpdatedEvent>,
    INotificationHandler<BlogDeletedEvent>
{
    public async Task Handle(BlogCreatedEvent notification, CancellationToken cancellationToken)
    {
        LogEvent(notification);

        var BlogQueryModel = mapper.Map<BlogQueryModel>(notification);
        await synchronizeDb.UpsertAsync(BlogQueryModel, filter => filter.Id == BlogQueryModel.Id);
        await ClearCacheAsync(notification);
    }

    public async Task Handle(BlogDeletedEvent notification, CancellationToken cancellationToken)
    {
        LogEvent(notification);

        await synchronizeDb.DeleteAsync<BlogQueryModel>(filter => filter.Id == notification.Id);
        await ClearCacheAsync(notification);
    }

    public async Task Handle(BlogUpdatedEvent notification, CancellationToken cancellationToken)
    {
        LogEvent(notification);

        var BlogQueryModel = mapper.Map<BlogQueryModel>(notification);
        await synchronizeDb.UpsertAsync(BlogQueryModel, filter => filter.Id == BlogQueryModel.Id);
        await ClearCacheAsync(notification);
    }

    private async Task ClearCacheAsync(BlogBaseEvent @event)
    {
        var cacheKeys = new[] { nameof(GetAllBlogQuery), $"{nameof(GetBlogByIdQuery)}_{@event.Id}" };
        await cacheService.RemoveAsync(cacheKeys);
    }

    private void LogEvent<TEvent>(TEvent @event) where TEvent : BlogBaseEvent =>
        logger.LogInformation("----- Triggering the event {EventName}, model: {EventModel}", typeof(TEvent).Name, @event.ToJson());
}