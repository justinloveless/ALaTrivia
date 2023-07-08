using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ALaTrivia.Models.Mongo;

public class TriviaTopic
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    [BsonElement("Category")]
    public TriviaCategory? Category { get; set; }
    [BsonElement("Name")]
    public string? Name { get; set; }
}