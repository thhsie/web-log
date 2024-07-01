using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using Weblog.Query.Abstractions;
using Weblog.Query.QueriesModel;

namespace Weblog.Query.Data.Mappings;

public class BlogMap : IReadDbMapping
{
    public void Configure()
    {
        // TryRegisterClassMap: Registers a class map if it is not already registered.
        BsonClassMap.TryRegisterClassMap<BlogQueryModel>(classMap =>
        {
            classMap.AutoMap();
            classMap.SetIgnoreExtraElements(true);

            classMap.MapMember(Blog => Blog.Id)
                .SetIsRequired(true);

            classMap.MapMember(Blog => Blog.UserId)
                .SetIsRequired(true);  

            classMap.MapMember(Blog => Blog.Title)
                .SetIsRequired(true);

            classMap.MapMember(Blog => Blog.Content)
                .SetIsRequired(true);

            classMap.MapMember(Blog => Blog.LastUpdated)
                .SetIsRequired(true)
                .SetSerializer(new DateTimeSerializer(true));

            // Ignore
            classMap.UnmapMember(Blog => Blog.TruncatedContent);
        });
    }
}