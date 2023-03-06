using Microsoft.AspNetCore.Mvc;
using PharmaMgmtApi.Commons.Utils;
using PharmaMgmtApi.Interfaces.Services;
using PharmaMgmtApi.ViewModels.Orders;

namespace PharmaMgmtApi.Controllers;

[Route("api/orders")]
[ApiController]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrdersController(IOrderService orderService)
    {
        _orderService = orderService;
    }
    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] PaginationParams @params)
    {
        return Ok(await _orderService.GetAllAsync(expression: null, @params));
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromForm] OrderCreateViewModel orderCreateViewModel)
    {
        var userId = Guid.Parse(HttpContext.User.FindFirst("Id")?.Value ?? "0");
        return Ok(await _orderService.CreateAsync(userId, orderCreateViewModel));
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        return Ok(await _orderService.DeleteAsync(order => order.Id == id));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync(Guid id)
    {
        var userId = Guid.Parse(HttpContext.User.FindFirst("Id")?.Value ?? "0");
        return Ok(await _orderService.GetAsync(userId, order => order.Id == id));
    }
}