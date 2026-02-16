namespace _2026_Roomify_Backend.DTOs
{
    public class UpdateBookingUserDto
    {
        public string NamaPeminjam { get; set; }
        public string NoKontak { get; set; }
        public DateTime Tanggal { get; set; }
        public string JamMulai { get; set; }
        public string JamSelesai { get; set; }
        public string Keperluan { get; set; }
        public int BuildingId { get; set; }
        public int RoomId { get; set; }
    }

    public class UpdateStatusDto
    {
        public string Status { get; set; }
    }
}
