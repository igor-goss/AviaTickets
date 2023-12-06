using Microsoft.EntityFrameworkCore;
using Ticket.Persistence.Exceptions;
using Ticket.Persistence.Repositories.Interfaces;

namespace Ticket.Persistence.Repositories.Implementations
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        protected readonly AppDbContext _dbContext;

        protected Repository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            var result = await _dbContext.Set<T>().FindAsync(id);

            return result == null ? throw new EntityNotFoundException($"Entity with ID {id} not found.") : result;
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            var result = await _dbContext.Set<T>().ToListAsync();

            return result;
        }

        public virtual async Task AddAsync(T entity)
        {
            var result = await _dbContext.Set<T>().AddAsync(entity);

            await _dbContext.SaveChangesAsync();

            return;
        }

        public virtual async Task UpdateAsync(T entity)
        {
            _dbContext.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;

            await _dbContext.SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(int id)
        {
            var entity = await _dbContext.Airports.FindAsync(id);

            if (entity == null)
            {
                throw new EntityNotFoundException($"Entity with ID {id} not found.");
            }

            _dbContext.Airports.Remove(entity);

            await _dbContext.SaveChangesAsync();
        }
    }
}
