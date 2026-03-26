using FlexiDesk.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexiDesk.Domain.Interfaces
{
    public interface IResourceRepository:IRepository<Resource>
    {
        public Task<IEnumerable<Resource>> GetAllWithDapperAsync(CancellationToken ct);
        public Task<Resource?> GetByNameAsync(string name, CancellationToken ct);
        public Task<Resource?> GetResourceWithReservationsAsync(Guid id, CancellationToken ct = default);
        public Task<IEnumerable<Resource>> SearchResourcesAsync(string? name, decimal? maxPrice, CancellationToken ct = default);
    }
}
