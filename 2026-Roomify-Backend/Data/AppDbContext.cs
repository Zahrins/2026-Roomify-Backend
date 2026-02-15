using _2026_Roomify_Backend.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema; 

namespace _2026_Roomify_Backend.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Building> Buildings { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<BookingStatusHistory> BookingStatusHistories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Username = "zahrin", PasswordHash = "password123" }
            );

            modelBuilder.Entity<Building>().ToTable("buildings");
            modelBuilder.Entity<Room>().ToTable("rooms");

            modelBuilder.Entity<Room>()
                .HasOne(r => r.Building)
                .WithMany(b => b.Rooms)
                .HasForeignKey(r => r.BuildingId);

            modelBuilder.Entity<BookingStatusHistory>()
                .HasOne(h => h.Booking)
                .WithMany()
                .HasForeignKey(h => h.BookingId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<BookingStatusHistory>()
                .HasOne(h => h.ChangedBy)
                .WithMany()
                .HasForeignKey(h => h.ChangedByUserId)
                .OnDelete(DeleteBehavior.SetNull);

            var buildings = new[]
            {
                new Building { Id = 1, Nama = "Gedung D4" },
                new Building { Id = 2, Nama = "Gedung D3" },
                new Building { Id = 3, Nama = "Gedung SAW" },
                new Building { Id = 4, Nama = "Gedung Pasca" }
            };

            modelBuilder.Entity<Building>().HasData(buildings);

            var rooms = new[]
            {
                new Room { Id = 1, Nama = "R.101", Tipe = "Kelas", Kapasitas = 30, Status = "kosong", BuildingId = 1 },
                new Room { Id = 2, Nama = "R.102", Tipe = "Kelas", Kapasitas = 20, Status = "kosong", BuildingId = 1 },
                new Room { Id = 3, Nama = "R.103", Tipe = "Kelas", Kapasitas = 20, Status = "kosong", BuildingId = 1 },
                new Room { Id = 4, Nama = "R.104", Tipe = "Kelas", Kapasitas = 20, Status = "kosong", BuildingId = 1 },
                new Room { Id = 5, Nama = "Lab.IT", Tipe = "Laboratorium", Kapasitas = 20, Status = "kosong", BuildingId = 1 },

                new Room { Id = 6, Nama = "R.201", Tipe = "Kelas", Kapasitas = 20, Status = "kosong", BuildingId = 2 },
                new Room { Id = 7, Nama = "R.202", Tipe = "Kelas", Kapasitas = 25, Status = "kosong", BuildingId = 2 },
                new Room { Id = 8, Nama = "Lab IoT", Tipe = "Laboratorium", Kapasitas = 15, Status = "kosong", BuildingId = 2 },
                new Room { Id = 9, Nama = "R.203", Tipe = "Kelas", Kapasitas = 30, Status = "kosong", BuildingId = 2 },

                new Room { Id = 10, Nama = "Ruang 301", Tipe = "Kelas", Kapasitas = 40, Status = "kosong", BuildingId = 3 },
                new Room { Id = 11, Nama = "Ruang 302", Tipe = "Kelas", Kapasitas = 35, Status = "kosong", BuildingId = 3 },
                new Room { Id = 12, Nama = "Lab Multimedia", Tipe = "Laboratorium", Kapasitas = 20, Status = "kosong", BuildingId = 3 },

                new Room { Id = 13, Nama = "Ruang 401", Tipe = "Kelas", Kapasitas = 25, Status = "kosong", BuildingId = 4 },
                new Room { Id = 14, Nama = "Ruang 402", Tipe = "Kelas", Kapasitas = 25, Status = "kosong", BuildingId = 4 },
                new Room { Id = 15, Nama = "Lab Research", Tipe = "Laboratorium", Kapasitas = 15, Status = "kosong", BuildingId = 4 }
            };

            modelBuilder.Entity<Room>().HasData(rooms);
        }

    }
}