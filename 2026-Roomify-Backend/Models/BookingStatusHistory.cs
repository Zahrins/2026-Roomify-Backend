namespace _2026_Roomify_Backend.Models
{
    public class BookingStatusHistory
    {
        public int Id { get; set; }
        public int BookingId { get; set; }
        public string Status { get; set; }
        public DateTime ChangedAt { get; set; }
        public int? ChangedByUserId { get; set; }

        
        public Booking Booking { get; set; }
        public User ChangedBy { get; set; }
    }
}
