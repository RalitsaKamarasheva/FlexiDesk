using FlexiDesk.Domain.Entities;
using FlexiDesk.Domain.Interfaces;
using FlexiDesk.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexiDesk.Infrastructure.Repositories
{
    public class ReservationRepository(FlexiDeskContext context) : EFRepository<Reservation>(context), IReservationRepository
    {
        private readonly FlexiDeskContext _context=context;
        public async Task<bool> IsResourceBookedAsync(Guid resourceId, DateTime startDate, DateTime endDate, CancellationToken ct = default)
        {
            return await _context.Reservations.AnyAsync(r =>
            r.ResourceId == resourceId &&
            r.StartTime < endDate && r.EndTime > startDate, ct);
        }
    }
}
