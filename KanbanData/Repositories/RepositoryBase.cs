using System.Threading.Tasks;
using K.Data.MSSql;

namespace K.Data.Repositories
{
    /// <summary>
    /// Base repository abstract class for common persistence operations.
    /// </summary>
    public abstract class RepositoryBase
    {
        protected readonly KanbanDbContext Context;

        protected RepositoryBase(KanbanDbContext context)
        {
            Context = context;
        }

        /// <summary>
        /// Saves pending changes to the database.
        /// </summary>
        protected Task PersistAsync() => Context.SaveChangesAsync();

        /// <summary>
        /// Clears the EF Core change tracker (useful for rollbacks or fresh queries).
        /// </summary>
        protected void ClearTracking() => Context.ChangeTracker.Clear();
    }
}
