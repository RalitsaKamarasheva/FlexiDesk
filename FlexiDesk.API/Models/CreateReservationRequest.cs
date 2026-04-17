namespace FlexiDesk.API.Models
{
    public class CreateReservationRequest
    {
        public Guid ResourceId { get; set; }
        public string UserID { get; set; } = string.Empty;
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
