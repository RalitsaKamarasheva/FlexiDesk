using FlexiDesk.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexiDesk.Domain.Interfaces
{
    public interface IReservationRepository:IRepository<Reservation>
    {
        Task<bool> IsResourceBookedAsync(Guid resourceId,DateTime startDate,DateTime endDate, CancellationToken ct=default);
    }
}
