using PharmaMgmtApi.ViewModels.Users;

namespace PharmaMgmtApi.Interfaces.Services;

public interface IAccountService
{
    Task<string> EmailVerifyAsync(EmailAddress emailAddress);
    
    Task<bool> RegisterAsync(UserCreateViewModel userCreateViewModel);
    
    Task<string> LoginAsync(UserLoginModel userLoginModel);
}