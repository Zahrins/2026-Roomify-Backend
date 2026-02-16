using _2026_Roomify_Backend.Data;
using _2026_Roomify_Backend.Models;
using _2026_Roomify_Backend.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

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
        [Authorize] 
        public async Task<ActionResult<IEnumerable<Booking>>> GetBookings([FromQuery] string tanggal)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userRole = User.FindFirst(ClaimTypes.Role)?.Value?.ToLower();

            if (string.IsNullOrEmpty(userIdClaim))
            {
                return Unauthorized(new { message = "Token tidak valid atau User ID tidak ditemukan." });
            }

            int userId = int.Parse(userIdClaim);

            var query = _context.Bookings.Include(b => b.Room).AsQueryable();

            if (userRole != "admin")
            {
                query = query.Where(b => b.UserId == userId);
            }

            var bookings = await query.ToListAsync();

            return Ok(bookings);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<Booking>> GetBooking(int id)
        {
            var booking = await _context.Bookings
                .Include(b => b.Room)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (booking == null)
            {
                return NotFound(new { message = "Booking tidak ditemukan" });
            }

            var currentUserId = GetCurrentUserId();
            var userRole = User.FindFirst(ClaimTypes.Role)?.Value?.ToLower();

            if (userRole != "admin" && booking.UserId != currentUserId)
            {
                return Forbid();
            }

            return Ok(booking);
        }

        [HttpPut("user/{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateBookingByUser(int id, [FromBody] UpdateBookingUserDto dto)
        {
            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null) return NotFound(new { message = "Booking tidak ditemukan" });

            var currentUserId = GetCurrentUserId();

            Console.WriteLine($"DEBUG: ID di Database: {booking.UserId} | ID dari Token: {currentUserId}");

            if (booking.UserId != currentUserId)
            {
                return Forbid(); 
            }

            booking.NamaPeminjam = dto.NamaPeminjam;
            booking.NoKontak = dto.NoKontak;
            booking.Tanggal = DateTime.SpecifyKind(dto.Tanggal, DateTimeKind.Utc); 
            booking.JamMulai = dto.JamMulai;
            booking.JamSelesai = dto.JamSelesai;
            booking.Keperluan = dto.Keperluan;
            booking.BuildingId = dto.BuildingId;
            booking.RoomId = dto.RoomId;

            await _context.SaveChangesAsync();
            return Ok(new { message = "Booking berhasil diupdate." });
        }

        [HttpPut("admin/{id}/status")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateBookingStatus(int id, [FromBody] UpdateStatusDto dto)
        {
            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null)
                return NotFound();

            if (booking.Status != dto.Status)
            {
                var history = new BookingStatusHistory
                {
                    BookingId = id,
                    Status = dto.Status,
                    ChangedAt = DateTime.UtcNow,
                    ChangedByUserId = GetCurrentUserId()
                };

                _context.BookingStatusHistories.Add(history);
                booking.Status = dto.Status;
            }

            await _context.SaveChangesAsync();
            return Ok(new { message = "Status berhasil diupdate oleh admin." });
        }

        [HttpGet("{id}/history")]
        public async Task<ActionResult<List<BookingStatusHistory>>> GetBookingHistory(int id)
        {
            var history = await _context.BookingStatusHistories
                .Where(h => h.BookingId == id)
                .OrderBy(h => h.ChangedAt)
                .ToListAsync();

            return Ok(history);
        }

        private int GetCurrentUserId()
        {
            var userIdClaim = User.FindFirst("userId")?.Value;

            if (string.IsNullOrEmpty(userIdClaim))
            {
                userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            }

            return userIdClaim != null ? int.Parse(userIdClaim) : 0;
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