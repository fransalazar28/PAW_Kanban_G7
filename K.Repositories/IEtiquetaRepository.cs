using K.Models.DTOs;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace K.Repositories
{
    public interface IEtiquetaRepository
    {
        Task<List<EtiquetaMiniDto>> GetByBoardAsync(int tableroId, CancellationToken ct = default);
        Task<bool> ExistsByNameAsync(int tableroId, string nombre, CancellationToken ct = default);
        Task<EtiquetaMiniDto> CreateAsync(CreateEtiquetaDto dto, CancellationToken ct = default);
        Task AddToHistoriaAsync(int historiaId, int etiquetaId, CancellationToken ct = default);
        Task RemoveFromHistoriaAsync(int historiaId, int etiquetaId, CancellationToken ct = default);
        Task<List<EtiquetaMiniDto>> GetByHistoriaAsync(int historiaId, CancellationToken ct = default);
        Task SetForHistoriaAsync(int historiaId, IEnumerable<int> etiquetaIds, CancellationToken ct = default);
        Task DeleteAsync(int etiquetaId, CancellationToken ct = default);

    }
}
