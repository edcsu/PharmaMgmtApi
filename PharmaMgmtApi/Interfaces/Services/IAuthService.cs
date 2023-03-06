using PharmaMgmtApi.Models;

namespace PharmaMgmtApi.Interfaces.Services;

public interface IAuthService
{
    string GenerateToken(User user);
}