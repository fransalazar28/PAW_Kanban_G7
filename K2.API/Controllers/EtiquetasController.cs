using Microsoft.AspNetCore.Mvc;
using K.Business;       
using K.Models.DTOs;  

namespace K2.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EtiquetasController : ControllerBase
{
    private readonly IEtiquetaService _svc;
    public EtiquetasController(IEtiquetaService svc) => _svc = svc;


    [HttpGet("board/{tableroId:int}")]
    public async Task<ActionResult<IEnumerable<EtiquetaMiniDto>>> GetByBoard(
        int tableroId,
        CancellationToken ct)
        => Ok(await _svc.GetByBoardAsync(tableroId, ct));

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        await _svc.DeleteAsync(id, ct);
        return NoContent();
    }



    [HttpPost]
    public async Task<ActionResult<EtiquetaMiniDto>> Create(
        [FromBody] CreateEtiquetaDto dto,
        CancellationToken ct)
    {
        try
        {
            var created = await _svc.CreateAsync(dto, ct);
            return CreatedAtAction(nameof(GetByBoard), new { tableroId = dto.TableroId }, created);
        }
        catch (InvalidOperationException ex) 
        {
            return Conflict(ex.Message);
        }
    }
}
