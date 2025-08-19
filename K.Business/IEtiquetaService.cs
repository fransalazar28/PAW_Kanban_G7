using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using K.Models.DTOs;

namespace K.Business
{
    public interface IEtiquetaService
    {
        Task<List<EtiquetaMiniDto>> GetByBoardAsync(int tableroId, CancellationToken ct = default);
        Task<EtiquetaMiniDto> CreateAsync(CreateEtiquetaDto dto, CancellationToken ct = default);
        Task AddToHistoriaAsync(int historiaId, int etiquetaId, CancellationToken ct = default);
        Task RemoveFromHistoriaAsync(int historiaId, int etiquetaId, CancellationToken ct = default);
        Task<List<EtiquetaMiniDto>> GetByHistoriaAsync(int historiaId, CancellationToken ct = default);
        Task SetForHistoriaAsync(int historiaId, IEnumerable<int> etiquetaIds, CancellationToken ct = default);
        Task DeleteAsync(int etiquetaId, CancellationToken ct = default);

    }
}
