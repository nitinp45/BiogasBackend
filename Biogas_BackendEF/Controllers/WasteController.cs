using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Biogas_BackendEF.Models;

namespace Biogas_BackendEF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WasteController : ControllerBase
    {
        private readonly BiogasDataBaseContext _context;

        public WasteController(BiogasDataBaseContext context)
        {
            _context = context;
        }

        // GET: api/Waste
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WasteContributor>>> GetWasteContributors()
        {
            return await _context.WasteContributors.ToListAsync();
        }

        // GET: api/Waste/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WasteContributor>> GetWasteContributor(int id)
        {
            var wasteContributor = await _context.WasteContributors.FindAsync(id);

            if (wasteContributor == null)
            {
                return NotFound();
            }

            return wasteContributor;
        }

        // PUT: api/Waste/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWasteContributor(int id, WasteContributor wasteContributor)
        {
            if (id != wasteContributor.ContributorId)
            {
                return BadRequest();
            }

            _context.Entry(wasteContributor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WasteContributorExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Waste
        [HttpPost]
        public async Task<ActionResult<WasteContributor>> PostWasteContributor(WasteContributor wasteContributor)
        {
            // Check if the UserId exists in the Users table
            var userExists = await _context.Users.AnyAsync(u => u.UId == wasteContributor.userId);
            if (!userExists)
            {
                return BadRequest("User with the specified ID does not exist.");
            }

            _context.WasteContributors.Add(wasteContributor);

            
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWasteContributor", new { id = wasteContributor.ContributorId }, wasteContributor);
        }


        // DELETE: api/Waste/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWasteContributor(int id)
        {
            var wasteContributor = await _context.WasteContributors.FindAsync(id);
            if (wasteContributor == null)
            {
                return NotFound();
            }

            _context.WasteContributors.Remove(wasteContributor);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("test/{userId}")]
        public async Task<IActionResult> TestUser(int userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UId == userId); // Or u.UserId if renamed

            if (user == null)
            {
                return NotFound("User not found (test)");
            }

            return Ok(user);
        }


        private bool WasteContributorExists(int id)
        {
            return _context.WasteContributors.Any(e => e.ContributorId == id);
        }
    }
}
