using K.Models;

namespace K.Repositories
{
    public interface IWidgetConfigRepository
    {

        Task<IEnumerable<WidgetConfig>> GetAllAsync(string userId);
        Task<WidgetConfig?> GetByIdAsync(int id);
        Task AddAsync(WidgetConfig widget);
        Task UpdateAsync(WidgetConfig widget);
        Task DeleteAsync(int id);

    }
}
