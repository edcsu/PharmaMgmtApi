using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PharmaMgmtApi.Interfaces.Services;
using PharmaMgmtApi.ViewModels.Users;

namespace PharmaMgmtApi.Controllers;

[Route("api/accounts")]
[ApiController]
public class AccountsController : ControllerBase
{
    private readonly IAccountService _accountService;

    public AccountsController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpPost("register"), AllowAnonymous]
    public async Task<IActionResult> CreateAsync([FromForm] UserCreateViewModel user)
    {
        return Ok(await _accountService.RegisterAsync(user));
    }

    [HttpPost("login"), AllowAnonymous]
    public async Task<IActionResult> LoginAsync([FromForm] UserLoginModel user)
    {
        return Ok(new { Token = await _accountService.LoginAsync(user) });
    }

    [HttpPost("emailVerify"), AllowAnonymous]
    public async Task<IActionResult> EmailVerify([FromForm] EmailAddress emailAddress)
    {
        return Ok(new { Token = await _accountService.EmailVerifyAsync(emailAddress) });
    }
}