using Microsoft.EntityFrameworkCore;
using _2026_Roomify_Backend.Models;
using System.ComponentModel.DataAnnotations.Schema; 

namespace _2026_Roomify_Backend.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Building> Gedungs { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Building>().ToTable("buildings");
            modelBuilder.Entity<Room>().ToTable("rooms");

            modelBuilder.Entity<Building>()
                .HasMany(b => b.Rooms)
                .WithOne()
                .HasForeignKey(r => r.BuildingId); 
        }
    }
}