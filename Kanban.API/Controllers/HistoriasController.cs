using K.Data.MSSql;
using K.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace K.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HistoriasController : ControllerBase
{
    private readonly KanbanDbContext _db;
    public HistoriasController(KanbanDbContext db) => _db = db;

    // =========== GET tablero completo ===========
    // GET api/historias/board/{tableroId}
    [HttpGet("board/{tableroId:int}")]
    public async Task<IActionResult> GetBoard(int tableroId)
    {
        var board = await _db.Tableros
            .Where(t => t.TableroId == tableroId)
            .Select(t => new
            {
                t.TableroId,
                t.Titulo,
                Columnas = t.Columnas
                    .OrderBy(c => c.Orden)
                    .Select(c => new
                    {
                        c.ColumnaId,
                        c.Nombre,
                        Historias = c.Historias
                            .OrderBy(h => h.Orden)
                            .Select(h => new
                            {
                                h.HistoriaId,
                                h.Titulo,
                                h.Descripcion,
                                h.Orden,
                                h.ResponsableId
                            })
                            .ToList()
                    })
                    .ToList()
            })
            .FirstOrDefaultAsync();

        return board is null ? NotFound() : Ok(board);
    }

    // =========== POST crear historia ===========
    public record CreateHistoriaDto(string Titulo, string? Descripcion, int ColumnaId, int? ResponsableId);

    // POST api/historias
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateHistoriaDto dto)
    {
        var orden = await _db.Historias.CountAsync(h => h.ColumnaId == dto.ColumnaId) + 1;

        var h = new Historia
        {
            Titulo = dto.Titulo,
            Descripcion = dto.Descripcion,
            ColumnaId = dto.ColumnaId,
            Orden = orden,
            ResponsableId = dto.ResponsableId
        };

        _db.Historias.Add(h);
        await _db.SaveChangesAsync();

        return Created($"/api/historias/{h.HistoriaId}", new { h.HistoriaId });
    }

    // =========== PATCH mover historia (drag & drop) ===========
    public record MoveHistoriaDto(int ColumnaId, int NewIndex);

    // PATCH api/historias/{id}/status
    [HttpPatch("{id:int}/status")]
    public async Task<IActionResult> Move(int id, [FromBody] MoveHistoriaDto dto)
    {
        var story = await _db.Historias.FindAsync(id);
        if (story is null) return NotFound();

        var fromCol = story.ColumnaId;
        story.ColumnaId = dto.ColumnaId;

        // Reordenar destino
        var target = await _db.Historias
            .Where(x => x.ColumnaId == dto.ColumnaId && x.HistoriaId != id)
            .OrderBy(x => x.Orden)
            .ToListAsync();

        var insertAt = Math.Clamp(dto.NewIndex, 0, target.Count);
        target.Insert(insertAt, story);
        for (int i = 0; i < target.Count; i++)
            target[i].Orden = i + 1;

        // Normalizar origen si cambió de columna
        if (fromCol != dto.ColumnaId)
        {
            var source = await _db.Historias
                .Where(x => x.ColumnaId == fromCol)
                .OrderBy(x => x.Orden)
                .ToListAsync();

            for (int i = 0; i < source.Count; i++)
                source[i].Orden = i + 1;
        }

        await _db.SaveChangesAsync();
        return NoContent();
    }
}
