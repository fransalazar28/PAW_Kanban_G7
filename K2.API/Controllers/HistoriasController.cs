using Microsoft.AspNetCore.Mvc;
using K.Business;            
using K.Models.DTOs;         

namespace K.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HistoriasController : ControllerBase
{
    private readonly IHistoriaService _svc;
    public HistoriasController(IHistoriaService svc) => _svc = svc;

    [HttpGet("board/{tableroId:int}")]
    public async Task<ActionResult<BoardDto>> GetBoard(int tableroId)
    {
        var board = await _svc.GetBoardAsync(tableroId);
        return board is null ? NotFound() : Ok(board);
    }


    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateHistoriaDto dto)
    {
        var id = await _svc.CreateAsync(dto);
        return Created($"/api/historias/{id}", new { historiaId = id });
    }


    [HttpPatch("{id:int}/status")]
    public async Task<IActionResult> Move(int id, [FromBody] MoveHistoriaDto dto)
    {
        await _svc.MoveAsync(id, dto.ColumnaId, dto.NewIndex);
        return NoContent();
    }


    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateHistoriaDto dto)
    {
        await _svc.UpdateAsync(id, dto);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _svc.DeleteAsync(id);
        return NoContent();
    }
}
