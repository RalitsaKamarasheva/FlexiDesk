using FlexiDesk.Domain.Entities;
using FlexiDesk.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FlexiDesk.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService _service;

        public ReservationController(IReservationService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create(Reservation reservation, CancellationToken ct)
        {
            // 1. Първо ползваме твоята Senior проверка в модела
            if (!reservation.IsValid())
            {
                return BadRequest("Invalid reservation times. Start must be in the future and before End time.");
            }

            // 2. Викаме сервиза, който ще провери дали ресурсът е свободен (Бизнес логика)
            var success = await _service.BookResourceAsync(reservation, ct);

            if (!success)
            {
                return Conflict("The resource is already booked for this time period.");
            }

            return Ok(new { Message = "Reservation created successfully!", ReservationId = reservation.Id });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
        {
            var reservation = await _service.GetReservationByIdAsync(id, ct);
            return reservation == null ? NotFound() : Ok(reservation);
        }
    }
}
