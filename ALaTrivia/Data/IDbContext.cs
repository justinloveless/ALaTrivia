using ALaTrivia.Models.Mongo;
using MongoDB.Driver;

namespace ALaTrivia.Data;

public interface IDbContext
{
    IMongoCollection<TriviaQuestion> TriviaQuestions { get; }
}