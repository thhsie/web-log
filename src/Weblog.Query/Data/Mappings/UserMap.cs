using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using Weblog.Query.Abstractions;
using Weblog.Query.QueriesModel;

namespace Weblog.Query.Data.Mappings;

public class UserMap : IReadDbMapping
{
    public void Configure()
    {
        // TryRegisterClassMap: Registers a class map if it is not already registered.
        BsonClassMap.TryRegisterClassMap<UserQueryModel>(classMap =>
        {
            classMap.AutoMap();
            classMap.SetIgnoreExtraElements(true);

            classMap.MapMember(User => User.Id)
                .SetIsRequired(true);

            classMap.MapMember(User => User.FirstName)
                .SetIsRequired(true);

            classMap.MapMember(User => User.LastName)
                .SetIsRequired(true);

            classMap.MapMember(User => User.Gender)
                .SetIsRequired(true);

            classMap.MapMember(User => User.Email)
                .SetIsRequired(true);

            classMap.MapMember(User => User.DateOfBirth)
                .SetIsRequired(true)
                .SetSerializer(new DateTimeSerializer(true));

            // Ignore
            classMap.UnmapMember(User => User.FullName);
        });
    }
}