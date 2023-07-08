using ALaTrivia.Models.Mongo;

namespace ALaTrivia.Repository;

public interface ITriviaQuestionRepository
{
    Task<IEnumerable<TriviaQuestion>> GetQuestions();
    Task<TriviaQuestion> GetQuestion(string id);
    Task<IEnumerable<TriviaQuestion>> GetQuestionsByTag(string tag);

    Task CreateTriviaQuestion(TriviaQuestion question);
    Task BatchCreateTriviaQuestions(IEnumerable<TriviaQuestion> questions);
    Task<bool> UpdateTriviaQuestion(TriviaQuestion question);
    Task<bool> DeleteTriviaQuestion(string id);
}