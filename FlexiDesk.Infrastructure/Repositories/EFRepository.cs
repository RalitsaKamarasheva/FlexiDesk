using FlexiDesk.Domain.Entities;
using FlexiDesk.Domain.Interfaces;
using FlexiDesk.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FlexiDesk.Infrastructure.Repositories
{
    public class EFRepository<T>(FlexiDeskContext context) : IRepository<T> where T : class
    {
        private readonly FlexiDeskContext _context = context;


        public async Task<IEnumerable<T>> GetAllAsync(CancellationToken ct=default)
        {
            return await _context.Set<T>().ToListAsync(ct);
        }

        public async Task<T?> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            return await _context.Set<T>().FindAsync(new object[] { id }, ct);

        }

        public async Task AddAsync(T entity, CancellationToken ct = default)
        {
            await _context.Set<T>().AddAsync(entity, ct);
            await _context.SaveChangesAsync(ct);
        }

        public async Task DeleteAsync(Guid id, CancellationToken ct = default)
        {
            var resource = await _context.Set<T>().FindAsync(new object[] { id }, ct, ct);
            if (resource != null)
            {
                _context.Set<T>().Remove(resource);
                await _context.SaveChangesAsync(ct);
            }
        }

        public async Task UpdateAsync(T entity, CancellationToken ct = default)
        {
            // Маркираме обекта като променен
            _context.Set<T>().Update(entity);

            // Записваме промените асинхронно
            await _context.SaveChangesAsync(ct);
        }
    }
}
