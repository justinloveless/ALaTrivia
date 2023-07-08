using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ALaTrivia.Models.Mongo;

public class TriviaQuestion
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    [BsonElement("Question")] public string? Question { get; set; }
    [BsonElement("Tags")]public IEnumerable<string>? Tags { get; set; }
    [BsonElement("Answer")] public string? Answer { get; set; }
    [BsonElement("Options")]public IEnumerable<string>? Options { get; set; }
    [BsonElement("Verified")] public bool HasBeenReviewed { get; set; }
}