using FlexiDesk.Domain.Entities;
using FlexiDesk.Domain.Interfaces;
using FlexiDesk.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexiDesk.Infrastructure.Handlers
{
    public class AuditLogHandler(FlexiDeskContext context) : IDomainEventHandler<Reservation>
    {
        public async Task HandleAsync(Reservation reservation, CancellationToken ct = default)
        {
            var log = new AuditLog
            {
                EntityName = nameof(Reservation),
                EntityId = reservation.Id,
                Action = "Created",
                Details = $"Потребител {reservation.UserID} резервира ресурс {reservation.ResourceId} от {reservation.StartTime} до {reservation.EndTime}."
            };
            await context.AuditLogs.AddAsync(log, ct);
            await context.SaveChangesAsync(ct);

            Console.WriteLine($"[AuditLog]: Успешно записан лог за резервация {reservation.Id}");
        }
    }
}
