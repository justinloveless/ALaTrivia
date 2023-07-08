using ALaTrivia.Models.Mongo;
using MongoDB.Driver;

namespace ALaTrivia.Data;

public interface ITriviaQuestionContext
{
    IMongoCollection<TriviaQuestion> TriviaQuestions { get; }
}