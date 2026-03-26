using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexiDesk.Domain.Entities
{
    public class Reservation
    {
        public Guid Id { get; set; }
        public Guid ResourceId {get; set; }
        public string UserID { get; set; } = string.Empty;
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public DateTime CreatedAt { get; set; }=DateTime.UtcNow;

        public Resource? Resource { get; set; }

        // Senior стъпка: Валидация на ниво модел
        public bool IsValid() => StartTime < EndTime && StartTime > DateTime.UtcNow;
    }
}
