using Microsoft.AspNetCore.Mvc;
using _2026_Roomify_Backend.Data;
using _2026_Roomify_Backend.Models;
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

        [HttpPost]
        public async Task<IActionResult> CreateBooking([FromBody] Booking booking)
        {
            try
            {
                booking.Tanggal = DateTime.SpecifyKind(booking.Tanggal, DateTimeKind.Utc);

                _context.Bookings.Add(booking);

                await _context.SaveChangesAsync();

                return Ok(new { message = "Booking berhasil disimpan!" });
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR DATABASE: " + (ex.InnerException?.Message ?? ex.Message));

                return BadRequest(new { message = ex.InnerException?.Message ?? ex.Message });
            }
        }
    }
}