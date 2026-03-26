using Dapper;
using FlexiDesk.Domain.Entities;
using FlexiDesk.Domain.Interfaces;
using FlexiDesk.Infrastructure.Persistence;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace FlexiDesk.Infrastructure.Repositories
{
    public class ResourceRepository(FlexiDeskContext context, IDbConnectionFactory<SqlConnection> dbConnection)
        : EFRepository<Resource>(context), IResourceRepository
    {
        private readonly IDbConnectionFactory<SqlConnection> _dbConnection;
        private readonly FlexiDeskContext _context =context;
        // Тук оставяш само методите, които ползват Dapper (от SQLQueries.resx)
        public async Task<IEnumerable<Resource>> GetAllWithDapperAsync(CancellationToken ct)
        {
            using var db = dbConnection.Get("FlexiDesk");
            return await db.QueryAsync<Resource>(new CommandDefinition(SQLQueries.GetAllResources, cancellationToken: ct));
        }

        public async Task<Resource?> GetByNameAsync(string name, CancellationToken ct)
        {
            using var db = dbConnection.Get("FlexiDesk");
            return await db.QueryFirstOrDefaultAsync<Resource>(new CommandDefinition(SQLQueries.GetResourceByName, new { Name=name}, cancellationToken: ct));
        }

        public async Task<Resource?> GetResourceWithReservationsAsync(Guid id, CancellationToken ct = default)
        {
            return await _context.Resources
            .Include(r => r.Reservations)
            .FirstOrDefaultAsync(r => r.Id == id, ct);
        }

        public async Task<IEnumerable<Resource>> SearchResourcesAsync(string? name, decimal? maxPrice, CancellationToken ct = default)
        {
            using var db = dbConnection.Get("FlexiDesk");
            return await db.QueryAsync<Resource>(new CommandDefinition(SQLQueries.GetResourcesByNameAndPrice, new
            {
                Name = name,
                MaxPrice = maxPrice
            }, cancellationToken: ct));
        }
    }
}
