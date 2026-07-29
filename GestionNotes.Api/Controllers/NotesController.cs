using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using GestionNotes.Core.Models;
using GestionNotes.Core.Services;

namespace GestionNotes.Api.Controllers;

[ApiController]
[Route("api/notes")]
[Authorize]
public class NotesController : ControllerBase
{
    private readonly INoteService _noteService;

    public NotesController(INoteService noteService)
    {
        _noteService = noteService;
    }

    private int CurrentRoleId => int.Parse(User.FindFirstValue(ClaimTypes.Role) ?? "0");
    private Guid CurrentUserId => Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? Guid.Empty.ToString());

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] NoteModel model)
    {
        var result = await _noteService.CreateAsync(model);
        if (!result.Success)
            return BadRequest(new { message = result.Message });

        return Ok(result.Data);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 20)
    {
        var result = await _noteService.GetAllAsync(page, pageSize);
        return Ok(result.Data);
    }

    [HttpGet("eleve/{eleveId}")]
    public async Task<IActionResult> GetByEleve(Guid eleveId)
    {
        var result = await _noteService.GetByEleveIdAsync(eleveId, CurrentUserId, CurrentRoleId);
        if (!result.Success)
            return Forbid();

        return Ok(result.Data);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _noteService.GetByIdAsync(id);
        if (!result.Success)
            return NotFound(new { message = result.Message });

        return Ok(result.Data);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] NoteModel model)
    {
        var result = await _noteService.UpdateAsync(id, model);
        if (!result.Success)
            return NotFound(new { message = result.Message });

        return Ok(result.Data);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _noteService.DeleteAsync(id);
        if (!result.Success)
            return NotFound(new { message = result.Message });

        return NoContent();
    }
}
