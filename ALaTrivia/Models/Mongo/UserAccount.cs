using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ALaTrivia.Models.Mongo;

public class UserAccount
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("Username")]
    public string? Username { get; set; }
    [BsonElement("Email")]
    public string? Email { get; set; }
    [BsonElement("DisplayName")]
    public string? DisplayName { get; set; }
    // store list of favorite topics
    [BsonElement("FavoriteTopics")] public List<TriviaTopic>? FavoriteTopics { get; set; } = new ();
}