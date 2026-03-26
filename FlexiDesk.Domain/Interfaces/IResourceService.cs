using FlexiDesk.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexiDesk.Domain.Interfaces
{
    public interface IResourceService
    {
        public Task<IEnumerable<Resource>> GetAllResourcesAsync(CancellationToken ct = default);
        public Task<Resource?> GetResourceByIdAsync(Guid id, CancellationToken ct = default);
        public Task<bool> CreateResourceAsync(Resource resource, CancellationToken ct = default); // Връща bool, за да знаем дали е успешно
        public Task UpdateResourceAsync(Resource resource, CancellationToken ct = default);
        public Task DeleteResourceAsync(Guid id, CancellationToken ct = default);
        public Task<Resource?> GetResourceWithReservationsAsync(Guid id, CancellationToken ct = default);
        public Task<IEnumerable<Resource>> SearchResourcesAsync(string? name,decimal? maxPrice,  CancellationToken ct = default);
    }
}
