using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using GestionNotes.Core.Models;
using GestionNotes.Core.Services;

namespace GestionNotes.Api.Controllers;

[ApiController]
[Route("api/matieres")]
[Authorize]
public class MatieresController : ControllerBase
{
    private readonly IMatiereService _matiereService;

    public MatieresController(IMatiereService matiereService)
    {
        _matiereService = matiereService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] MatiereModel model)
    {
        var result = await _matiereService.CreateAsync(model);
        if (!result.Success)
            return BadRequest(new { message = result.Message });

        return Ok(result.Data);
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetAll()
    {
        var result = await _matiereService.GetAllAsync();
        return Ok(result.Data);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] MatiereModel model)
    {
        var result = await _matiereService.UpdateAsync(id, model);
        if (!result.Success)
            return NotFound(new { message = result.Message });

        return Ok(result.Data);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _matiereService.DeleteAsync(id);
        if (!result.Success)
            return NotFound(new { message = result.Message });

        return NoContent();
    }
}
