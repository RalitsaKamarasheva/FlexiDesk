using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexiDesk.Domain.Entities
{
    public class Resource
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = "Desk";
        public decimal PricePerHour { get; set; }
        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
    }
}
