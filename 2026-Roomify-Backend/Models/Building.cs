using System.ComponentModel.DataAnnotations.Schema;

namespace _2026_Roomify_Backend.Models
{
    [Table("buildings")] 
    public class Building
    {
        [Column("id")] 
        public int Id { get; set; }

        [Column("nama")]
        public string Nama { get; set; } = string.Empty;

        [NotMapped] 
        public string Tanggal { get; set; } = string.Empty;

        public List<Room> Rooms { get; set; } = new List<Room>();
    }
}