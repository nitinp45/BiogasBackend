using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Biogas_BackendEF.Models;
using System.Security.Cryptography;
using System.Text;

namespace Biogas_BackendEF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly BiogasDataBaseContext _context;

        public UsersController(BiogasDataBaseContext context)
        {
            _context = context;
        }

        
 

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.UId)
            {
                return BadRequest("User ID mismatch.");
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound("User not found.");
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

      

        // Register API
        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(User user)
        {
            // Check if user already exists
            if (_context.Users.Any(u => u.Email == user.Email))
            {
                return Conflict("User already exists.");
            }

            // Hash password
            user.Password = HashPassword(user.Password);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUser), new { id = user.UId }, user);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == loginDto.Email);

            if (user == null || !VerifyPassword(loginDto.Password, user.Password))
            {
                return Unauthorized("Invalid email or password.");
            }

            // Store user data in session
            HttpContext.Session.SetInt32("UserId", user.UId); // Store User ID
            HttpContext.Session.SetString("UserName", user.Username); // Store Email
            HttpContext.Session.SetString("Role", user.Role);
            return Ok(new { message = "Login successful", userId = user.UId,userName=user.Username,Role=user.Role});
        }


        private bool VerifyPassword(string inputPassword, string storedHashedPassword)
        {
            return HashPassword(inputPassword) == storedHashedPassword;
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear(); // Clear session data
            return Ok(new { message = "Logout successful" });
        }


        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.UId == id);
        }
    }
}
