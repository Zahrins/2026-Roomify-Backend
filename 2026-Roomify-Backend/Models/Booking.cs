using System.ComponentModel.DataAnnotations.Schema;

namespace _2026_Roomify_Backend.Models
{
    [Table("bookings")]
    public class Booking
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("nama_peminjam")]
        public string NamaPeminjam { get; set; } = string.Empty;
        [Column("no_kontak")]
        public string NoKontak { get; set; } = string.Empty;
        [Column("tanggal")]
        public DateTime Tanggal { get; set; }
        [Column("jam_mulai")]
        public string JamMulai { get; set; } = string.Empty;
        [Column("jam_selesai")]
        public string JamSelesai { get; set; } = string.Empty;
        [Column("keperluan")]
        public string Keperluan { get; set; } = string.Empty;
        [Column("building_id")]
        public int BuildingId { get; set; }
        [Column("room_id")]
        public int RoomId { get; set; }
    }
}
