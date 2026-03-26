using FlexiDesk.Domain.Entities;
using FlexiDesk.Domain.Interfaces;
using FlexiDesk.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexiDesk.Infrastructure.Services
{
    public class ReservationService(IReservationRepository repository) : IReservationService
    {
        private readonly IReservationRepository _repository=repository;
        public async Task<bool> BookResourceAsync(Reservation reservation, CancellationToken ct = default)
        {
            // 1. Проверка: Краят трябва да е след началото
            if (reservation.EndTime <= reservation.StartTime) return false;

            // 2. Проверка: Дали не е в миналото
            if (reservation.StartTime < DateTime.UtcNow) return false;

            // 3. Проверка: Свободно ли е бюрото? (Ползваме репозиторито)
            bool isBooked = await _repository.IsResourceBookedAsync(
                reservation.ResourceId, reservation.StartTime, reservation.EndTime, ct);

            if (isBooked) return false;

            // 4. Ако всичко е наред - записваме
            await _repository.AddAsync(reservation, ct);
            return true;
        }

        public async Task<Reservation?> GetReservationByIdAsync(Guid id, CancellationToken ct = default)
        {
            return await _repository.GetByIdAsync(id, ct);
        }
    }
}

