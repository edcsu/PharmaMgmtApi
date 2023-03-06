using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PharmaMgmtApi.Commons.Utils;
using PharmaMgmtApi.Interfaces.Services;
using PharmaMgmtApi.ViewModels.Users;

namespace PharmaMgmtApi.Controllers;

[Route("api/users")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet, AllowAnonymous]
    public async Task<IActionResult> GetAllAsync([FromQuery] PaginationParams @params)
    {
        return Ok(await _userService.GetAllAsync(expression: null, @params));
    }

    [HttpDelete("{id:guid}"), Authorize(Roles = "User, Admin")]
    public async Task<IActionResult> DeleteAsync()
    {
        var id = Guid.Parse(HttpContext.User.FindFirst("Id")?.Value ?? "0");
        return Ok(await _userService.DeleteAsync(user => user.Id == id));
    }

    [HttpPut, Authorize(Roles = "User, Admin")]
    public async Task<IActionResult> UpdateAsync([FromForm] UserCreateViewModel userCreateViewModel)
    {
        var id = Guid.Parse(HttpContext.User.FindFirst("Id")?.Value ?? "0");
        return Ok(await _userService.UpdateAsync(id, userCreateViewModel));
    }

    [HttpGet("Info"), Authorize(Roles = "User, Admin")]
    public async Task<IActionResult> GetInfoAsync()
    {
        var id = Guid.Parse(HttpContext.User.FindFirst("Id")?.Value ?? "0");

        return Ok(await _userService.GetAsync(user => user.Id == id));
    }
}