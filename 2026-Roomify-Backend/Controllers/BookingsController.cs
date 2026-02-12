using _2026_Roomify_Backend.Data;
using _2026_Roomify_Backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace _2026_Roomify_Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BookingsController(AppDbContext context)
        {
            _context = context;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateBooking([FromBody] Booking booking)
        {
            Console.WriteLine("AUTH HEADER: " + Request.Headers["Authorization"]);

            try
            {
                var userIdClaim = User.FindFirst("userId")?.Value;

                if (userIdClaim == null)
                    return Unauthorized(new { message = "User tidak valid" });

                booking.UserId = int.Parse(userIdClaim);

                booking.Tanggal = DateTime.SpecifyKind(booking.Tanggal, DateTimeKind.Utc);

                _context.Bookings.Add(booking);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Booking berhasil disimpan!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.InnerException?.Message ?? ex.Message });
            }
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Booking>>> GetBookings()
        {
            var bookings = await _context.Bookings
            .Include(b => b.Room) 
            .ToListAsync();

            return Ok(bookings);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Booking>> GetBookingById(int id)
        {
            var booking = await _context.Bookings
            .Include(b => b.Room) 
            .FirstOrDefaultAsync(b => b.Id == id);

            if (booking == null)
                return NotFound(new { message = "Booking tidak ditemukan" });

            return Ok(booking);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBooking(int id, [FromBody] Booking updatedBooking)
        {
            var booking = await _context.Bookings.FindAsync(id);

            if (booking == null)
                return NotFound(new { message = "Booking tidak ditemukan" });

            booking.NamaPeminjam = updatedBooking.NamaPeminjam;
            booking.NoKontak = updatedBooking.NoKontak;
            booking.Tanggal = DateTime.SpecifyKind(updatedBooking.Tanggal, DateTimeKind.Utc);
            booking.JamMulai = updatedBooking.JamMulai;
            booking.JamSelesai = updatedBooking.JamSelesai;
            booking.Keperluan = updatedBooking.Keperluan;
            booking.BuildingId = updatedBooking.BuildingId;
            booking.RoomId = updatedBooking.RoomId;

            var buildingExists = await _context.Buildings
        .AnyAsync(b => b.Id == updatedBooking.BuildingId);

                var roomExists = await _context.Rooms
                    .AnyAsync(r => r.Id == updatedBooking.RoomId);

                if (!buildingExists || !roomExists)
                {
                    return BadRequest(new { message = "Building atau Room tidak valid." });
                }

                await _context.SaveChangesAsync();

                return Ok(new { message = "Booking berhasil diperbarui!" });
            }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBooking(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null) return NotFound();

            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Data berhasil dihapus!" });
        }
    }
}