using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using K.Data.MSSql;
using K.Models;
using K.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace K.Repositories
{
    public class HistoriaRepository : IHistoriaRepository
    {
        private readonly KanbanDbContext _db;
        public HistoriaRepository(KanbanDbContext db) => _db = db;

        public Task<BoardDto?> GetBoardAsync(int tableroId, CancellationToken ct = default)
        {
            return _db.Tableros
                .AsNoTracking()
                .Where(t => t.TableroId == tableroId)
                .Select(t => new BoardDto
                {
                    TableroId = t.TableroId,
                    Titulo = t.Titulo,
                    Columnas = t.Columnas
                        .OrderBy(c => c.Orden)
                        .Select(c => new ColumnDto
                        {
                            ColumnaId = c.ColumnaId,
                            Nombre = c.Nombre,
                            Historias = c.Historias
                                .OrderBy(h => h.Orden)
                                .Select(h => new HistoriaItemDto
                                {
                                    HistoriaId = h.HistoriaId,
                                    Titulo = h.Titulo,
                                    Descripcion = h.Descripcion,
                                    Orden = h.Orden,
                                    ResponsableId = h.ResponsableId,
                                    ResponsableNombre = h.Responsable != null ? h.Responsable.NombreUsuario : null,
                                    FechaVencimiento = h.FechaVencimiento,
                                    Comentarios = h.Comentarios.Count(),
                                    Etiquetas = h.HistoriasEtiquetas  
                                        .Select(he => new EtiquetaMiniDto
                                        {
                                            EtiquetaId = he.EtiquetaId,
                                            Nombre = he.Etiqueta.Nombre,
                                            Color = he.Etiqueta.Color
                                        })
                                        .ToList()
                                })
                                .ToList()
                        })
                        .ToList()
                })
                .FirstOrDefaultAsync(ct);
        }


        public async Task<int> CreateAsync(CreateHistoriaDto dto, CancellationToken ct = default)
        {
            var last = await _db.Historias
                .Where(h => h.ColumnaId == dto.ColumnaId)
                .MaxAsync(h => (int?)h.Orden, ct) ?? 0;

            var h = new Historia
            {
                Titulo = dto.Titulo,
                Descripcion = dto.Descripcion,
                ColumnaId = dto.ColumnaId,
                Orden = last + 1,
                ResponsableId = dto.ResponsableId
            };

            _db.Historias.Add(h);
            await _db.SaveChangesAsync(ct);
            return h.HistoriaId;
        }

        public async Task MoveAsync(int historiaId, int columnaId, int newIndex, CancellationToken ct = default)
        {
            var story = await _db.Historias.FirstOrDefaultAsync(x => x.HistoriaId == historiaId, ct);
            if (story is null) return;

            var fromCol = story.ColumnaId;
            var toCol = columnaId;

            if (fromCol == toCol)
            {
                var list = await _db.Historias
                    .Where(x => x.ColumnaId == fromCol && x.HistoriaId != historiaId)
                    .OrderBy(x => x.Orden)
                    .ToListAsync(ct);

                var insertAt = Math.Clamp(newIndex, 0, list.Count);
                list.Insert(insertAt, story);

                for (int i = 0; i < list.Count; i++)
                    list[i].Orden = i + 1;
            }
            else
            {
                var source = await _db.Historias
                    .Where(x => x.ColumnaId == fromCol && x.HistoriaId != historiaId)
                    .OrderBy(x => x.Orden)
                    .ToListAsync(ct);

                for (int i = 0; i < source.Count; i++)
                    source[i].Orden = i + 1;

                var target = await _db.Historias
                    .Where(x => x.ColumnaId == toCol)
                    .OrderBy(x => x.Orden)
                    .ToListAsync(ct);

                var insertAt = Math.Clamp(newIndex, 0, target.Count);
                story.ColumnaId = toCol;
                target.Insert(insertAt, story);

                for (int i = 0; i < target.Count; i++)
                    target[i].Orden = i + 1;
            }

            await _db.SaveChangesAsync(ct);
        }

        public async Task UpdateAsync(int id, UpdateHistoriaDto dto, CancellationToken ct = default)
        {
            var h = await _db.Historias.FindAsync(new object?[] { id }, ct);
            if (h is null) return;

            if (!string.IsNullOrWhiteSpace(dto.Titulo))
                h.Titulo = dto.Titulo.Trim();

            if (dto.Descripcion is not null)
                h.Descripcion = dto.Descripcion;

            if (dto.ResponsableId != null || h.ResponsableId != null)
                h.ResponsableId = dto.ResponsableId;

            if (dto.FechaVencimiento != null || h.FechaVencimiento != null)
                h.FechaVencimiento = dto.FechaVencimiento;

            await _db.SaveChangesAsync(ct);
        }

        public async Task DeleteAsync(int id, CancellationToken ct = default)
        {
            var h = await _db.Historias.FindAsync(new object?[] { id }, ct);
            if (h is null) return;

            var colId = h.ColumnaId;

            _db.Historias.Remove(h);
            await _db.SaveChangesAsync(ct);

            var left = await _db.Historias
                .Where(x => x.ColumnaId == colId)
                .OrderBy(x => x.Orden)
                .ToListAsync(ct);

            for (int i = 0; i < left.Count; i++)
                left[i].Orden = i + 1;

            await _db.SaveChangesAsync(ct);
        }
    }
}
