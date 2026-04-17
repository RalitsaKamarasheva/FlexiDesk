using FlexiDesk.Domain.Entities;
using FlexiDesk.Domain.Interfaces;
using FlexiDesk.Infrastructure.Repositories;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexiDesk.Infrastructure.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _repository;
        private readonly IEnumerable<IDomainEventHandler<Reservation>> _handlers;
        private readonly IValidator<Reservation> _validator;

        public ReservationService(IReservationRepository repository, IEnumerable<IDomainEventHandler<Reservation>> eventHandler, IValidator<Reservation> validator)
        {
            _repository = repository;
            _handlers = eventHandler;
            _validator= validator;
        }
        
        public async Task<Reservation> BookReservationAsync(Reservation reservation, CancellationToken ct = default)
        {
            var validationResult= await _validator.ValidateAsync(reservation, ct);

            if (!validationResult.IsValid)
            {
                // Събираме всички грешки в един текст
                var errors = string.Join(" ", validationResult.Errors.Select(e => e.ErrorMessage));
                throw new Exception(errors); // Middleware-ът ще го улови
            }

            // 3. Проверка: Свободно ли е бюрото? (Ползваме репозиторито)
            bool isBooked = await _repository.IsResourceBookedAsync(
                reservation.ResourceId, reservation.StartTime, reservation.EndTime, ct);

            if (isBooked) return null;

            // 4. Ако всичко е наред - записваме
            await _repository.AddAsync(reservation, ct);

            var tasks = _handlers.Select(async handler =>
            {
                try
                {
                    await handler.HandleAsync(reservation, ct);
                }
                catch (Exception ex)
                {
                    // Тук е важно да логнеш грешката, но да не спираш приложението
                    Console.WriteLine($"Грешка в {handler.GetType().Name}: {ex.Message}");
                }
            });

            await Task.WhenAll(tasks);

            return reservation;
        }

        public async Task<Reservation?> GetReservationByIdAsync(Guid id, CancellationToken ct = default)
        {
            return await _repository.GetByIdWithResourceAsync(id, ct);
        }
    }
}

