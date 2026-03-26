using FlexiDesk.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexiDesk.Domain.Interfaces
{
    public interface IReservationService
    {
        public Task<bool> BookResourceAsync(Reservation reservation, CancellationToken ct = default);
        public Task<Reservation?> GetReservationByIdAsync(Guid id, CancellationToken ct=default);
    }
}
