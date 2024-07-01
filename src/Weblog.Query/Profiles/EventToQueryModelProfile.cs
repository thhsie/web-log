using AutoMapper;
using Weblog.Domain.Entities.BlogAggregate.Events;
using Weblog.Domain.Entities.UserAggregate.Events;
using Weblog.Query.QueriesModel;

namespace Weblog.Query.Profiles;

public class EventToQueryModelProfile : Profile
{
    public EventToQueryModelProfile()
    {
        #region User
        CreateMap<UserCreatedEvent, UserQueryModel>(MemberList.Destination)
            .ConstructUsing(@event => CreateUserQueryModel(@event));

        CreateMap<UserUpdatedEvent, UserQueryModel>(MemberList.Destination)
            .ConstructUsing(@event => CreateUserQueryModel(@event));

        CreateMap<UserDeletedEvent, UserQueryModel>(MemberList.Destination)
            .ConstructUsing(@event => CreateUserQueryModel(@event));
        #endregion

        #region Blog
        CreateMap<BlogCreatedEvent, BlogQueryModel>(MemberList.Destination)
            .ConstructUsing(@event => CreateBlogQueryModel(@event));

        CreateMap<BlogUpdatedEvent, BlogQueryModel>(MemberList.Destination)
            .ConstructUsing(@event => CreateBlogQueryModel(@event));

        CreateMap<BlogDeletedEvent, BlogQueryModel>(MemberList.Destination)
            .ConstructUsing(@event => CreateBlogQueryModel(@event));
        #endregion
    }

    public override string ProfileName => nameof(EventToQueryModelProfile);

    private static UserQueryModel CreateUserQueryModel<TEvent>(TEvent @event) where TEvent : UserBaseEvent =>
        new(@event.Id, @event.FirstName, @event.LastName, @event.Gender.ToString(), @event.Email, @event.DateOfBirth);

    private static BlogQueryModel CreateBlogQueryModel<TEvent>(TEvent @event) where TEvent : BlogBaseEvent =>
        new(@event.Id, @event.UserId, @event.Title, @event.Content, @event.LastUpdated);
}