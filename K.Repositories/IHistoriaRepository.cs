using K.Models.DTOs;
using System.Threading;
using System.Threading.Tasks;

namespace K.Repositories
{
    public interface IHistoriaRepository
    {
        Task<BoardDto?> GetBoardAsync(int tableroId, CancellationToken ct = default);
        Task<int> CreateAsync(CreateHistoriaDto dto, CancellationToken ct = default);
        Task MoveAsync(int historiaId, int columnaId, int newIndex, CancellationToken ct = default);
        Task UpdateAsync(int id, UpdateHistoriaDto dto, CancellationToken ct = default);
        Task DeleteAsync(int id, CancellationToken ct = default);
    }
}
