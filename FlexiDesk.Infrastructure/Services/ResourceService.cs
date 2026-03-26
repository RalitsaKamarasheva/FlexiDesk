using FlexiDesk.Domain.Entities;
using FlexiDesk.Domain.Interfaces;

namespace FlexiDesk.Infrastructure.Services;

public class ResourceService : IResourceService
{
    private readonly IResourceRepository _repository;

    public ResourceService(IResourceRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Resource>> GetAllResourcesAsync(CancellationToken ct = default)
    {
        return await _repository.GetAllAsync(ct);
    }

    public async Task<Resource?> GetResourceByIdAsync(Guid id,CancellationToken ct=default)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<bool> CreateResourceAsync(Resource resource, CancellationToken ct = default)
    {
        // ПРИМЕРНА ЛОГИКА: Проверка за уникално име
        var existing = await _repository.GetByNameAsync(resource.Name,ct);
        if (existing != null)
        {
            // Тук в реална ситуация бихме върнали специфична грешка
            return false;
        }

        // Проверка за валидна цена
        if (resource.PricePerHour < 0) return false;

        await _repository.AddAsync(resource);
        return true;
    }

    public async Task UpdateResourceAsync(Resource resource, CancellationToken ct = default)
    {
        await _repository.UpdateAsync(resource);
    }

    public async Task DeleteResourceAsync(Guid id, CancellationToken ct = default)
    {
        await _repository.DeleteAsync(id,ct);
    }

    public async Task<Resource?> GetResourceWithReservationsAsync(Guid id, CancellationToken ct = default)
    {
        return await _repository.GetResourceWithReservationsAsync(id, ct);
    }

    public async Task<IEnumerable<Resource>> SearchResourcesAsync(string? name, decimal? maxPrice, CancellationToken ct = default)
    {
        return await _repository.SearchResourcesAsync(name, maxPrice, ct);
    }
}