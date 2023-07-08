using ALaTrivia.Models;
using ALaTrivia.Models.Mongo;
using MongoDB.Driver;

namespace ALaTrivia.Data;

public class TriviaQuestionContext : ITriviaQuestionContext
{
    public TriviaQuestionContext(TriviaDatabaseSettings settings)
    {
        var client = new MongoClient(settings.ConnectionString);
        var database = client.GetDatabase(settings.DatabaseName);

        TriviaQuestions = database.GetCollection<TriviaQuestion>(settings.CollectionName);
        // TriviaQuestionContextSeed.SeedData(TriviaQuestions); // we'll get to this later
    }

    public IMongoCollection<TriviaQuestion> TriviaQuestions { get; }
}