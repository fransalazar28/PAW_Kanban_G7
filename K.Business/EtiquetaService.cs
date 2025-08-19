using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using K.Models.DTOs;
using K.Repositories;

namespace K.Business
{
    public class EtiquetaService : IEtiquetaService
    {
        private readonly IEtiquetaRepository _repo;
        public EtiquetaService(IEtiquetaRepository repo) => _repo = repo;

        public Task<List<EtiquetaMiniDto>> GetByBoardAsync(int tableroId, CancellationToken ct = default)
            => _repo.GetByBoardAsync(tableroId, ct);

        public async Task<EtiquetaMiniDto> CreateAsync(CreateEtiquetaDto dto, CancellationToken ct = default)
        {
            if (await _repo.ExistsByNameAsync(dto.TableroId, dto.Nombre, ct))
                throw new System.InvalidOperationException("Ya existe una etiqueta con ese nombre en el tablero.");

            return await _repo.CreateAsync(dto, ct);
        }

        public Task AddToHistoriaAsync(int historiaId, int etiquetaId, CancellationToken ct = default)
            => _repo.AddToHistoriaAsync(historiaId, etiquetaId, ct);

        public Task RemoveFromHistoriaAsync(int historiaId, int etiquetaId, CancellationToken ct = default)
            => _repo.RemoveFromHistoriaAsync(historiaId, etiquetaId, ct);

        public Task<List<EtiquetaMiniDto>> GetByHistoriaAsync(int historiaId, CancellationToken ct = default)
    => _repo.GetByHistoriaAsync(historiaId, ct);

        public Task SetForHistoriaAsync(int historiaId, IEnumerable<int> etiquetaIds, CancellationToken ct = default)
            => _repo.SetForHistoriaAsync(historiaId, etiquetaIds, ct);
        public Task DeleteAsync(int etiquetaId, CancellationToken ct = default)
    => _repo.DeleteAsync(etiquetaId, ct);


    }
}
