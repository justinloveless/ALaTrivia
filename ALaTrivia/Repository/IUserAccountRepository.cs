using ALaTrivia.Models.Mongo;

namespace ALaTrivia.Repository;

public interface IUserAccountRepository
{
    Task<List<UserAccount>> GetAllUsers();
    Task<UserAccount> GetUser(string id);
    Task<UserAccount> GetUserByUsername(string username);
    Task<List<TriviaTopic>> GetFavoriteTopicsByUserId(string id);
    Task<bool> AddFavoriteTopic(string id, TriviaTopic topic);
    Task<bool> RemoveFavoriteTopic(string id, TriviaTopic topic);
    Task<bool> UpdateAccount(UserAccount account);
    Task CreateAccount(UserAccount account);
}