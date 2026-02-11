using System.ComponentModel.DataAnnotations.Schema;

namespace _2026_Roomify_Backend.Models
{
    [Table("rooms")]
    public class Room
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("nama")]
        public string Nama { get; set; } = string.Empty;

        [Column("status")]
        public string Status { get; set; } = "kosong";

        [Column("tipe")]
        public string Tipe { get; set; } = string.Empty;

        [Column("kapasitas")]
        public int Kapasitas { get; set; }
        public int BuildingId { get; set; }
        public Building? Building { get; set; }

    }
}