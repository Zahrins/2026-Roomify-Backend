using _2026_Roomify_Backend.Data;
using _2026_Roomify_Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace _2026_Roomify_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public UsersController(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterDto request)
        {
            // 1. Validasi input dasar
            if (string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Password))
            {
                return BadRequest(new { message = "Username dan password wajib diisi." });
            }

            if (_context.Users.Any(u => u.Username == request.Username))
            {
                return BadRequest(new { message = "Username sudah terdaftar." });
            }

            string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

            // 2. Proteksi Role (Hanya izinkan 'user' secara default)
            // Jika ingin membuat admin, kamu bisa buat endpoint khusus atau ubah manual di DB
            var role = "user";

            // Opsi: Hanya izinkan admin jika ada 'secret key' tertentu (opsional)
            if (request.Role?.Trim().ToLower() == "admin")
            {
                // role = "admin"; // Buka ini hanya jika kamu sedang testing
            }

            var newUser = new User
            {
                Username = request.Username,
                PasswordHash = passwordHash,
                Role = role
            };

            _context.Users.Add(newUser);
            _context.SaveChanges();

            return Ok(new { message = $"Registrasi sebagai {newUser.Role} berhasil!" });
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDto request)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == request.Username);

            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            {
                return BadRequest(new { message = "Username atau password salah." });
            }

            var jwtSettings = _configuration.GetSection("Jwt");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Pastikan user.Role di database isinya tepat "admin" atau "user"
            var claims = new[]
            {
        new Claim(ClaimTypes.Name, user.Username),
        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        new Claim(ClaimTypes.Role, user.Role), // POIN PENTING: Role dikunci di sini
        new Claim("userId", user.Id.ToString())
    };

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(3),
                signingCredentials: creds
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(new
            {
                message = "Login berhasil!",
                token = tokenString,
                username = user.Username,
                role = user.Role // Kirim role ke frontend agar frontend tahu harus buka menu apa
            });
        }
    }

    public class RegisterDto
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string? Role { get; set; } 
    }

    public class LoginDto
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
