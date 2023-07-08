using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ALaTrivia.Models.Mongo;

public class TriviaCategory
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonElement("Name")]
    public string? Name { get; set; }
}