

using BugTicketingSystem.DAL.Context;
using Microsoft.EntityFrameworkCore;

namespace BugTicketingSystem.DAL.Repositories.GenericRepo
{
    public class GenericRepo<T> : IGenericRepo<T> where T : class
    {
        private readonly BugDbContext _context;
        public GenericRepo(BugDbContext context)
        {
            _context = context;
        }
        public async Task<List<T>> GetAllAsync()
        {
            return await _context.Set<T>()
                .AsNoTracking().ToListAsync();
        }

        public async Task<T?> GetByIdAsync(Guid id)
        {
            return await _context.Set<T>()
                .FindAsync(id);
        }
        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        

        public void Update(T entity)
        {
            
        }
    }
}
