using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexiDesk.Domain.Entities
{
    public class AuditLog
    {
        public Guid Id { get; set; }
        public string EntityName { get; set; } = string.Empty; // "Reservation"
        public Guid EntityId { get; set; } // ID на резервацията
        public string Action { get; set; } = string.Empty; // "Created"
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public string Details { get; set; } = string.Empty; // "User user_123 booked resource X"
    }
}
