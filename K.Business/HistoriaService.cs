using K.Models.DTOs;
using K.Repositories;

namespace K.Business;

public class HistoriaService : IHistoriaService
{
    private readonly IHistoriaRepository _repo;
    public HistoriaService(IHistoriaRepository repo) => _repo = repo;

    public Task<BoardDto?> GetBoardAsync(int tableroId) => _repo.GetBoardAsync(tableroId);
    public Task<int> CreateAsync(CreateHistoriaDto dto) => _repo.CreateAsync(dto);
    public Task MoveAsync(int id, int columnaId, int newIndex) => _repo.MoveAsync(id, columnaId, newIndex);
    public Task UpdateAsync(int id, UpdateHistoriaDto dto) => _repo.UpdateAsync(id, dto);
    public Task DeleteAsync(int id) => _repo.DeleteAsync(id);
}
