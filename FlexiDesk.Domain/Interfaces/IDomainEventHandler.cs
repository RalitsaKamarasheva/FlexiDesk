using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexiDesk.Domain.Interfaces
{
    public interface IDomainEventHandler<in T> where T : class
    {
        Task HandleAsync(T domainEvent, CancellationToken ct = default);
    }
}
