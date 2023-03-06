using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PharmaMgmtApi.Commons.Utils;
using PharmaMgmtApi.Interfaces.Services;
using PharmaMgmtApi.ViewModels.Drugs;

namespace PharmaMgmtApi.Controllers;

[Route("api/drugs")]
[ApiController]
public class DrugsController : ControllerBase
{
    private readonly IDrugService _drugService;

    public DrugsController(IDrugService drugService)
    {
        _drugService = drugService;
    }

    [HttpGet, AllowAnonymous]
    public async Task<IActionResult> GetAllAsync([FromQuery] PaginationParams @params)
    {
        return Ok(await _drugService.GetAllAsync(expression: null, @params));
    }

    [HttpPost, AllowAnonymous]//, Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateAsync([FromBody] DrugCreateViewModel drugCreateViewModel)
    {
        return Ok(await _drugService.CreateAsync(drugCreateViewModel));
    }

    [HttpDelete("{id:guid}")]//, Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        return Ok(await _drugService.DeleteAsync(drug => drug.Id == id));
    }

    [HttpGet("{id:guid}"), AllowAnonymous]
    public async Task<IActionResult> GetAsync(Guid id)
    {
        return Ok(await _drugService.GetAsync(drug => drug.Id == id));
    }

    [HttpPut]//, Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] DrugCreateViewModel drugCreateViewModel)
    {
        return Ok(await _drugService.UpdateAsync(id, drugCreateViewModel));
    }
}