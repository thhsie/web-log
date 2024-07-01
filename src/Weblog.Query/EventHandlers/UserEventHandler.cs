using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Weblog.Core.Extensions;
using Weblog.Core.SharedKernel;
using Weblog.Domain.Entities.UserAggregate.Events;
using Weblog.Query.Abstractions;
using Weblog.Query.Application.User.Queries;
using Weblog.Query.QueriesModel;

namespace Weblog.Query.EventHandlers;

public class UserEventHandler(
    IMapper mapper,
    ISynchronizeDb synchronizeDb,
    ICacheService cacheService,
    ILogger<UserEventHandler> logger) :
    INotificationHandler<UserCreatedEvent>,
    INotificationHandler<UserUpdatedEvent>,
    INotificationHandler<UserDeletedEvent>
{
    public async Task Handle(UserCreatedEvent notification, CancellationToken cancellationToken)
    {
        LogEvent(notification);

        var UserQueryModel = mapper.Map<UserQueryModel>(notification);
        await synchronizeDb.UpsertAsync(UserQueryModel, filter => filter.Id == UserQueryModel.Id);
        await ClearCacheAsync(notification);
    }

    public async Task Handle(UserDeletedEvent notification, CancellationToken cancellationToken)
    {
        LogEvent(notification);

        await synchronizeDb.DeleteAsync<UserQueryModel>(filter => filter.Email == notification.Email);
        await ClearCacheAsync(notification);
    }

    public async Task Handle(UserUpdatedEvent notification, CancellationToken cancellationToken)
    {
        LogEvent(notification);

        var UserQueryModel = mapper.Map<UserQueryModel>(notification);
        await synchronizeDb.UpsertAsync(UserQueryModel, filter => filter.Id == UserQueryModel.Id);
        await ClearCacheAsync(notification);
    }

    private async Task ClearCacheAsync(UserBaseEvent @event)
    {
        var cacheKeys = new[] { nameof(GetAllUserQuery), $"{nameof(GetUserByIdQuery)}_{@event.Id}" };
        await cacheService.RemoveAsync(cacheKeys);
    }

    private void LogEvent<TEvent>(TEvent @event) where TEvent : UserBaseEvent =>
        logger.LogInformation("----- Triggering the event {EventName}, model: {EventModel}", typeof(TEvent).Name, @event.ToJson());
}