using _2026_Roomify_Backend.Data;
using _2026_Roomify_Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace _2026_Roomify_Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BuildingsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BuildingsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Building>>> GetBuildings([FromQuery] string tanggal)
        {
            var buildings = await _context.Gedungs
                .Include(b => b.Rooms)
                .ToListAsync();

            bool isWeekend = false;
            if (DateTime.TryParse(tanggal, out DateTime dt))
            {
                isWeekend = (dt.DayOfWeek == DayOfWeek.Saturday || dt.DayOfWeek == DayOfWeek.Sunday);
            }

            foreach (var building in buildings)
            {
                building.Tanggal = tanggal;
                foreach (var room in building.Rooms)
                {
                    if (isWeekend)
                    {
                        room.Status = "kosong";
                    }
                    else
                    {
                        if (room.Id % 2 == 0)
                        {
                            room.Status = "terpakai";
                        }
                        else
                        {
                            room.Status = "kosong";
                        }
                    }
                }
            }

            return Ok(buildings);
        }
    }
}