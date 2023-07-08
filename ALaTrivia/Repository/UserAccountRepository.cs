using ALaTrivia.Data;
using ALaTrivia.Models.Mongo;
using MongoDB.Driver;

namespace ALaTrivia.Repository;

public class UserAccountRepository : IUserAccountRepository
{
    private readonly IDbContext _context;

    public UserAccountRepository(IDbContext context)
    {
        _context = context;
    }

    public async Task<List<UserAccount>> GetAllUsers()
    {
        return await _context.UserAccounts.Find(Builders<UserAccount>.Filter.Empty)
            .ToListAsync();
    }

    public async Task<UserAccount> GetUser(string id)
    {
        return await (await _context.UserAccounts.FindAsync(
                x => x.Id.Equals(id)))
            .FirstOrDefaultAsync();
    }

    public async Task<UserAccount> GetUserByUsername(string username)
    {
        return await (await _context.UserAccounts.FindAsync(
                x => x.Username != null && x.Username.Equals(username)))
            .FirstOrDefaultAsync();
    }

    public async Task<List<TriviaTopic>> GetFavoriteTopicsByUserId(string id)
    {
        var result = await _context.UserAccounts.FindAsync(x => x.Id.Equals(id));
        var account = await result.FirstOrDefaultAsync();
        return account.FavoriteTopics ?? new List<TriviaTopic>();
    }

    public async Task<bool> AddFavoriteTopic(string id, TriviaTopic topic)
    {
        var filter = Builders<UserAccount>.Filter.Eq(x => x.Id, id);
        var push = Builders<UserAccount>.Update.Push(acc => 
            acc.FavoriteTopics, topic);
        var addFavoriteTopicResult = await _context.UserAccounts.UpdateOneAsync(filter, push);
        return addFavoriteTopicResult.IsAcknowledged && addFavoriteTopicResult.ModifiedCount > 0;
    }

    public async Task<bool> RemoveFavoriteTopic(string id, TriviaTopic topic)
    {
        var filter = Builders<UserAccount>.Filter.Eq(x => x.Id, id);
        var pull = Builders<UserAccount>.Update.PullFilter(acc => 
            acc.FavoriteTopics, t => topic.Id != null && topic.Id.Equals(t.Id));
        var addFavoriteTopicResult = await _context.UserAccounts.UpdateOneAsync(filter, pull);
        return addFavoriteTopicResult.IsAcknowledged && addFavoriteTopicResult.ModifiedCount > 0;
    }

    public async Task<bool> UpdateAccount(UserAccount account)
    {
        var updateResult =
            await _context.UserAccounts.ReplaceOneAsync(a => a.Id.Equals(account.Id), account);
        return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
    }

    public async Task CreateAccount(UserAccount account)
    {
        await _context.UserAccounts.InsertOneAsync(account);
    }
}