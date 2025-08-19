using Microsoft.AspNetCore.Mvc;
using K.Business;            
using K.Models.DTOs;
using K.Data.MSSql;
using K.Models;
using Microsoft.EntityFrameworkCore;

namespace K.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HistoriasController : ControllerBase
{
    private readonly IHistoriaService _svc;
    private readonly IEtiquetaService _tags; 

    public HistoriasController(IHistoriaService svc, IEtiquetaService tags) 
    {
        _svc = svc;
        _tags = tags;
    }

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



    [HttpPost("{historiaId:int}/etiquetas/{etiquetaId:int}")]
    public async Task<IActionResult> AddEtiqueta(int historiaId, int etiquetaId)
    {
        await _tags.AddToHistoriaAsync(historiaId, etiquetaId);
        return NoContent();
    }

    [HttpDelete("{historiaId:int}/etiquetas/{etiquetaId:int}")]
    public async Task<IActionResult> RemoveEtiqueta(int historiaId, int etiquetaId)
    {
        await _tags.RemoveFromHistoriaAsync(historiaId, etiquetaId);
        return NoContent();
    }
    // DTO para el PUT masivo
    public record UpdateEtiquetasRequest(List<int> EtiquetaIds);

    // GET: api/historias/{id}/etiquetas
    [HttpGet("{id:int}/etiquetas")]
    public async Task<ActionResult<IEnumerable<EtiquetaMiniDto>>> GetEtiquetasDeHistoria(
        int id,
        [FromServices] KanbanDbContext db,
        CancellationToken ct)
    {
        var list = await db.HistoriaEtiquetas
            .AsNoTracking()
            .Where(x => x.HistoriaId == id)
            .Select(x => new EtiquetaMiniDto
            {
                EtiquetaId = x.EtiquetaId,
                Nombre = x.Etiqueta.Nombre,
                Color = x.Etiqueta.Color
            })
            .OrderBy(e => e.Nombre)
            .ToListAsync(ct);

        return Ok(list);
    }

    // PUT: api/historias/{id}/etiquetas  (reemplaza todas)
    [HttpPut("{id:int}/etiquetas")]
    public async Task<IActionResult> SetEtiquetasDeHistoria(
        int id,
        [FromBody] UpdateEtiquetasRequest req,
        [FromServices] KanbanDbContext db,
        CancellationToken ct)
    {
        var nuevos = (req.EtiquetaIds ?? new List<int>()).Distinct().ToHashSet();

        var actuales = await db.HistoriaEtiquetas
            .Where(x => x.HistoriaId == id)
            .ToListAsync(ct);

        // quitar los que ya no estén
        var quitar = actuales.Where(x => !nuevos.Contains(x.EtiquetaId)).ToList();
        if (quitar.Count > 0) db.HistoriaEtiquetas.RemoveRange(quitar);

        // agregar faltantes
        var actualesIds = actuales.Select(x => x.EtiquetaId).ToHashSet();
        var agregar = nuevos.Except(actualesIds)
            .Select(etqId => new HistoriaEtiqueta { HistoriaId = id, EtiquetaId = etqId })
            .ToList();
        if (agregar.Count > 0) await db.HistoriaEtiquetas.AddRangeAsync(agregar, ct);

        await db.SaveChangesAsync(ct);
        return NoContent();
    }

}
