namespace FlexiDesk.API.Models
{
    public class ReservationResponse
    {
        public Guid Id { get; set; }
        public string ResourceName { get; set; } = string.Empty; // Можем да добавим името на ресурса тук!
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
