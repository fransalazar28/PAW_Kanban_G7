using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using K.Data.MSSql;
using K.Data.Repositories.Interfaces;

namespace K.Data.Repositories
{
    public class EfCoreGenericRepository<T> : RepositoryBase, IGenericRepository<T> where T : class
    {
        protected readonly DbSet<T> DbSet;

        public EfCoreGenericRepository(KanbanDbContext context) : base(context)
        {
            DbSet = context.Set<T>();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await DbSet.FindAsync(id);
        }

        public async Task<IEnumerable<T>> ListAsync()
        {
            return await DbSet.ToListAsync();
        }

        public async Task AddAsync(T entity)
        {
            await DbSet.AddAsync(entity);
            await PersistAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            DbSet.Update(entity);
            await PersistAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            DbSet.Remove(entity);
            await PersistAsync();
        }
    }
}