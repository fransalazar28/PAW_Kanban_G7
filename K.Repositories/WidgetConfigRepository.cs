using K.Models;
using KanbanData;
using Microsoft.EntityFrameworkCore;

namespace K.Repositories
{
    public class WidgetConfigRepository : IWidgetConfigRepository
    {
        private readonly ApplicationDbContext _context;

        public WidgetConfigRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<WidgetConfig>> GetAllAsync(string userId)
        {
            return await _context.WidgetConfigs
                .Where(w => w.UsuarioId == userId || w.Visibilidad == "público")
                .OrderByDescending(w => w,Favorito)
                .ToListAsync();
        }

        public async Task<WidgetConfig?> GetByIdAsync(int id)
        {
            return await _context.WidgetConfigs.FindAsync(id);
        }

        public async Task AddAsync(WidgetConfig widget)
        {
            _context.WidgetConfigs.Add(widget);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(WidgetConfig widget)
        {
            _context.WidgetConfigs.Update(widget);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var widget = await _context.WidgetConfigs.FindAsync(id);
            if (widget != null)
            {
                _context.WidgetConfigs.Remove(widget);
                await _context.SaveChangesAsync();
            }
        }
    }
}
