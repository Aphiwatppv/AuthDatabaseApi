using AuthDataAccess.Models;

namespace AuthDataAccess.Service
{
    public interface IAuthService
    {
        bool IsHashValid(string hashpass, string salt, string userpass);
        Task<string> LoginAsync(UserInput userInput);
        Task<string> RegisterMethod(RegisteringModel model);
        Task<ReturnHashSaltModel> ReturningHashSalt(string email);
    }
}