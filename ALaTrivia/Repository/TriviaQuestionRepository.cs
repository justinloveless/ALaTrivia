using ALaTrivia.Data;
using ALaTrivia.Models.Mongo;
using MongoDB.Driver;

namespace ALaTrivia.Repository;

public class TriviaQuestionRepository : ITriviaQuestionRepository
{
    private readonly ITriviaQuestionContext _context;

    public TriviaQuestionRepository(ITriviaQuestionContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<TriviaQuestion>> GetQuestions()
    {
        return await _context.TriviaQuestions.Find(q => true).ToListAsync();
    }

    public async Task<TriviaQuestion> GetQuestion(string id)
    {
        return await _context.TriviaQuestions.Find(q => q.Id == id).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<TriviaQuestion>> GetQuestionsByTag(string tag)
    {
        return await _context.TriviaQuestions.Find(q => q.Tags != null && q.Tags.Contains(tag)).ToListAsync();
    }

    public async Task CreateTriviaQuestion(TriviaQuestion question)
    {
        await _context.TriviaQuestions.InsertOneAsync(question);
    }

    public async Task BatchCreateTriviaQuestions(IEnumerable<TriviaQuestion> questions)
    {
        var triviaQuestions = questions.ToList();
        if (triviaQuestions.Any()) await _context.TriviaQuestions.InsertManyAsync(triviaQuestions);
    }

    public async Task<bool> UpdateTriviaQuestion(TriviaQuestion question)
    {
        var updateResult =
            await _context.TriviaQuestions.ReplaceOneAsync(filter: q => q.Id.Equals(question.Id), replacement: question);
        return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
    }

    public async Task<bool> DeleteTriviaQuestion(string id)
    {
        var deleteResult = await _context.TriviaQuestions.DeleteOneAsync(q => q.Id.Equals(id));
        return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
    }

}