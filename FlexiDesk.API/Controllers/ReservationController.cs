using AutoMapper;
using FlexiDesk.API.Models;
using FlexiDesk.Domain.Entities;
using FlexiDesk.Domain.Interfaces;
using FlexiDesk.Infrastructure.Repositories;
using FlexiDesk.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace FlexiDesk.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService _service;
        private readonly IMapper _mapper;

        public ReservationController(IReservationService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateReservationRequest request, CancellationToken ct)
        {
            var reservation = _mapper.Map<Reservation>(request);

            // 1. Записваме в базата
            var createdReservation = await _service.BookReservationAsync(reservation);

            if (createdReservation == null)
            {
                return BadRequest("Resource is already booked.");
            }

            // 2. ВАЖНО: Презареждаме обекта с неговия Resource от базата
            // Така AutoMapper ще види reservation.Resource.Name
            var fullReservation = await _service.GetReservationByIdAsync(createdReservation.Id);

            // 3. Мапваме към Response
            var response = _mapper.Map<ReservationResponse>(fullReservation);

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
        {
            var reservation = await _service.GetReservationByIdAsync(id, ct);
            return reservation == null ? NotFound() : Ok(reservation);
        }
    }
}
