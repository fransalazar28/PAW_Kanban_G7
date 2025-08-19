using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using K.Data.MSSql;
using K.Models;
using K.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace K.Repositories
{
    public class EtiquetaRepository : IEtiquetaRepository
    {
        private readonly KanbanDbContext _db;
        public EtiquetaRepository(KanbanDbContext db) => _db = db;

        public Task<List<EtiquetaMiniDto>> GetByBoardAsync(int tableroId, CancellationToken ct = default)
            => _db.Etiquetas
                  .AsNoTracking()
                  .Where(e => e.TableroId == tableroId)
                  .OrderBy(e => e.Nombre)
                  .Select(e => new EtiquetaMiniDto
                  {
                      EtiquetaId = e.EtiquetaId,
                      Nombre = e.Nombre,
                      Color = e.Color
                  })
                  .ToListAsync(ct);

        public Task<bool> ExistsByNameAsync(int tableroId, string nombre, CancellationToken ct = default)
            => _db.Etiquetas.AnyAsync(e => e.TableroId == tableroId && e.Nombre == nombre, ct);

        public async Task<EtiquetaMiniDto> CreateAsync(CreateEtiquetaDto dto, CancellationToken ct = default)
        {
            var e = new Etiqueta { TableroId = dto.TableroId, Nombre = dto.Nombre, Color = dto.Color };
            _db.Etiquetas.Add(e);
            await _db.SaveChangesAsync(ct);
            return new EtiquetaMiniDto { EtiquetaId = e.EtiquetaId, Nombre = e.Nombre, Color = e.Color };
        }

        public async Task AddToHistoriaAsync(int historiaId, int etiquetaId, CancellationToken ct = default)
        {
            var exists = await _db.HistoriaEtiquetas
                .AnyAsync(x => x.HistoriaId == historiaId && x.EtiquetaId == etiquetaId, ct);

            if (!exists)
            {
                _db.HistoriaEtiquetas.Add(new HistoriaEtiqueta { HistoriaId = historiaId, EtiquetaId = etiquetaId });
                await _db.SaveChangesAsync(ct);
            }
        }

        public async Task RemoveFromHistoriaAsync(int historiaId, int etiquetaId, CancellationToken ct = default)
        {
            var link = await _db.HistoriaEtiquetas
                .FirstOrDefaultAsync(x => x.HistoriaId == historiaId && x.EtiquetaId == etiquetaId, ct);

            if (link is null) return;

            _db.HistoriaEtiquetas.Remove(link);
            await _db.SaveChangesAsync(ct);
        }

        public async Task DeleteAsync(int etiquetaId, CancellationToken ct = default)
        {
            var e = await _db.Etiquetas.FirstOrDefaultAsync(x => x.EtiquetaId == etiquetaId, ct);
            if (e is null) return;


            _db.Etiquetas.Remove(e);
            await _db.SaveChangesAsync(ct);
        }

        public Task<List<EtiquetaMiniDto>> GetByHistoriaAsync(int historiaId, CancellationToken ct = default)
            => _db.HistoriaEtiquetas
                  .AsNoTracking()
                  .Where(x => x.HistoriaId == historiaId)
                  .Select(x => new EtiquetaMiniDto
                  {
                      EtiquetaId = x.EtiquetaId,
                      Nombre = x.Etiqueta.Nombre,
                      Color = x.Etiqueta.Color
                  })
                  .OrderBy(x => x.Nombre)
                  .ToListAsync(ct);

        public async Task SetForHistoriaAsync(int historiaId, IEnumerable<int> etiquetaIds, CancellationToken ct = default)
        {
            var nuevos = (etiquetaIds ?? Array.Empty<int>()).Distinct().ToHashSet();

            var actuales = await _db.HistoriaEtiquetas
                .Where(x => x.HistoriaId == historiaId)
                .ToListAsync(ct);


            var quitar = actuales.Where(x => !nuevos.Contains(x.EtiquetaId)).ToList();
            if (quitar.Count > 0) _db.HistoriaEtiquetas.RemoveRange(quitar);


            var actualesIds = actuales.Select(x => x.EtiquetaId).ToHashSet();
            var agregar = nuevos.Except(actualesIds)
                                .Select(id => new HistoriaEtiqueta { HistoriaId = historiaId, EtiquetaId = id })
                                .ToList();
            if (agregar.Count > 0) await _db.HistoriaEtiquetas.AddRangeAsync(agregar, ct);

            await _db.SaveChangesAsync(ct);
        }
    }
}
