using ALaTrivia.Models;
using ALaTrivia.Models.Mongo;
using MongoDB.Driver;

namespace ALaTrivia.Data;

public class DbContext : IDbContext
{
    public DbContext(TriviaDatabaseSettings settings)
    {
        var client = new MongoClient(settings.ConnectionString);
        var database = client.GetDatabase(settings.DatabaseName);

        TriviaQuestions = database.GetCollection<TriviaQuestion>(settings.QuestionCollection);
        // TriviaQuestionContextSeed.SeedData(TriviaQuestions); // we'll get to this later
    }

    public IMongoCollection<TriviaQuestion> TriviaQuestions { get; }
}