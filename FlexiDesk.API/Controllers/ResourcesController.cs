using FlexiDesk.Domain.Entities;
using FlexiDesk.Domain.Interfaces;
using FlexiDesk.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FlexiDesk.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ResourcesController : ControllerBase
    {
        private readonly IResourceService _service;

        public ResourcesController(IResourceService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var resources = await _service.GetAllResourcesAsync();
            return Ok(resources);
        }
        [HttpGet("{id}")]

        public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
        {
            var resource=await _service.GetResourceWithReservationsAsync(id, ct);
            return resource == null ? NotFound() : Ok(resource);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Resource resource)
        {
            var success = await _service.CreateResourceAsync(resource);
            if (!success) return BadRequest("Невалидни данни или името вече съществува.");

            return Ok(resource);
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string? name, [FromQuery] decimal? maxPrice, CancellationToken ct)
        {
            var results = await _service.SearchResourcesAsync(name, maxPrice, ct);
            return Ok(results);
        }

    }
}
