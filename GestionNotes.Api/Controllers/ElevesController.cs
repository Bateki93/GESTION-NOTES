using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using GestionNotes.Core.Models;
using GestionNotes.Core.Services;

namespace GestionNotes.Api.Controllers;

[ApiController]
[Route("api/eleves")]
[Authorize]
public class ElevesController : ControllerBase
{
    private readonly IEleveService _eleveService;

    public ElevesController(IEleveService eleveService)
    {
        _eleveService = eleveService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] RegisterRequest request)
    {
        var result = await _eleveService.CreateAsync(request);
        if (!result.Success)
            return BadRequest(new { message = result.Message });

        return Ok(result.Data);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 20)
    {
        var result = await _eleveService.GetAllAsync(page, pageSize);
        return Ok(result.Data);
    }

    [HttpGet("{userId}")]
    public async Task<IActionResult> GetByUserId(Guid userId)
    {
        var result = await _eleveService.GetByUserIdAsync(userId);
        if (!result.Success)
            return NotFound(new { message = result.Message });

        return Ok(result.Data);
    }

    [HttpPut("{userId}")]
    public async Task<IActionResult> Update(Guid userId, [FromBody] EleveModel model)
    {
        var result = await _eleveService.UpdateAsync(userId, model);
        if (!result.Success)
            return NotFound(new { message = result.Message });

        return Ok(result.Data);
    }

    [HttpDelete("{userId}")]
    public async Task<IActionResult> Delete(Guid userId)
    {
        var result = await _eleveService.DeleteAsync(userId);
        if (!result.Success)
            return NotFound(new { message = result.Message });

        return NoContent();
    }
}
