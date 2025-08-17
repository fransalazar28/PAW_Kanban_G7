using K.Models.DTOs;

namespace K.Business;

public interface IHistoriaService
{
    Task<BoardDto?> GetBoardAsync(int tableroId);
    Task<int> CreateAsync(CreateHistoriaDto dto);
    Task MoveAsync(int id, int columnaId, int newIndex);
    Task UpdateAsync(int id, UpdateHistoriaDto dto);
    Task DeleteAsync(int id);
}
