using ALaTrivia.Models.Mongo;
using ALaTrivia.Repository;
using Microsoft.AspNetCore.Mvc;

namespace ALaTrivia.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;
    private readonly IUserAccountRepository _accountRepository;

    public UserController(ILogger<UserController> logger, IUserAccountRepository accountRepository)
    {
        _logger = logger;
        _accountRepository = accountRepository;
    }

    [HttpGet]
    public async Task<List<UserAccount>> GetAllUserAccounts()
    {
        return await _accountRepository.GetAllUsers();
    }
    
    [HttpPost("/create")]
    public async Task CreateUser([FromBody] UserAccount account)
    {
        var newAccount = new UserAccount()
        {
            DisplayName = account.DisplayName,
            Email = account.Email,
            Username = account.Username
        };
        await _accountRepository.CreateAccount(newAccount);
    }

    [HttpGet("/get/id/{id}")]
    public async Task<UserAccount> GetById([FromRoute] string id)
    {
        return await _accountRepository.GetUser(id);
    }

    [HttpGet("/get/username/{username}")]
    public async Task<UserAccount> GetByUsername([FromRoute] string username)
    {
        return await _accountRepository.GetUserByUsername(username);
    }

    [HttpGet("/id/{id}/topics")]
    public async Task<List<TriviaTopic>> GetTopicsByUserId([FromRoute] string id)
    {
        return await _accountRepository.GetFavoriteTopicsByUserId(id);
    }

    [HttpPut("/id/{id}/topics/add")]
    public async Task<bool> AddFavoriteTopic([FromRoute] string id, [FromBody] TriviaTopic topic)
    {
        var newTopic = new TriviaTopic()
        {
            Name = topic.Name,
            Category = new TriviaCategory()
            {
                Name = topic.Category.Name
            }
        };
        return await _accountRepository.AddFavoriteTopic(id, newTopic);
    }

    [HttpDelete("/id/{id}/topics/remove")]
    public async Task<bool> RemoveFavoriteTopic([FromRoute] string id, TriviaTopic topic)
    {
        return await _accountRepository.RemoveFavoriteTopic(id, topic);
    }

    [HttpPut("/update")]
    public async Task<bool> UpdateAccount([FromBody] UserAccount account)
    {
        return await _accountRepository.UpdateAccount(account);
    }
    
    
}