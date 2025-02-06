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
    public class ProducersController : ControllerBase
    {
        private readonly BiogasDataBaseContext _context;

        public ProducersController(BiogasDataBaseContext context)
        {
            _context = context;
        }

       
        // DELETE: api/Producers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProducer(int id)
        {
            var producer = await _context.Producers.FindAsync(id);
            if (producer == null)
            {
                return NotFound();
            }

            _context.Producers.Remove(producer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // GET: api/Producers/AllWaste
        [HttpGet("AllWaste")]
        public async Task<ActionResult<IEnumerable<WasteContributor>>> GetAllWaste()
        {
            var wasteList = await _context.WasteContributors.ToListAsync();

            if (!wasteList.Any())
            {
                return NotFound("No waste records found.");
            }

            return Ok(wasteList);
        }


        [HttpPut("BuyWaste/{wasteId}/User/{userId}")]
        public async Task<IActionResult> BuyWaste(int wasteId, int userId)
        {
            // Find the waste contributor record by wasteId
            var wasteContributor = await _context.WasteContributors.FindAsync(wasteId);
            if (wasteContributor == null)
            {
                return NotFound("Waste record not found.");
            }

            // Find the user by userId
            var user = await _context.Users.FindAsync(userId);
            if (user == null || user.Role != "producer")
            {
                return BadRequest("Invalid user. Only users with the 'Producer' role can collect waste.");
            }

            // Check if producer exists in the Producers table
            var producer = await _context.Producers.FirstOrDefaultAsync(p => p.UserId == userId);

            // If producer does not exist, create a new Producer entry (this happens only when they buy waste for the first time)
            if (producer == null)
            {
                producer = new Producer
                {
                    UserId = userId,
                    ProductionCapacity = 0, // Default or update later
                    Status = "Active"
                };

                _context.Producers.Add(producer);
                await _context.SaveChangesAsync(); // Save to get ProducerId
            }

            // Check if the waste status is already 'Collected'
            if (wasteContributor.Status == "Collected")
            {
                return BadRequest("Waste has already been collected.");
            }

            // Update waste status and assign ProducerId to CollectedBy
            wasteContributor.Status = "Collected";
            wasteContributor.CollectedBy = producer.ProducerId;

            // Mark the entity as modified
            _context.Entry(wasteContributor).State = EntityState.Modified;

            try
            {
                // Save changes to the database
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest("Failed to update waste record due to concurrency issues.");
            }

            return Ok($"Waste successfully collected by Producer (ID: {producer.ProducerId}).");
        }

        [HttpPost("AddWasteToBiogas")]
        public async Task<IActionResult> AddWasteToBiogas([FromBody] Biogas biogas)
        {
            if (biogas == null)
            {
                return BadRequest("Invalid biogas data.");
            }

            // Check if the producer exists
            var producer = await _context.Producers.FindAsync(biogas.ProducerId);
            if (producer == null)
            {
                return NotFound("Producer not found.");
            }

            // Assign default values
            biogas.Status = "Processing"; // Initially, set status as "Processing"

            _context.Biogas.Add(biogas);
            await _context.SaveChangesAsync();

            // Use CreatedAtRoute or return the created biogas directly
            return Ok(biogas);  // Return the created biogas object directly
        }


        [HttpPut("UpdateBiogasStatus/{biogasId}")]
        public async Task<IActionResult> UpdateBiogasStatus(int biogasId)
        {
            // Find the biogas entry by ID
            var biogas = await _context.Biogas.FindAsync(biogasId);
            if (biogas == null)
            {
                return NotFound("Biogas entry not found.");
            }

            // Update status to "Available"
            biogas.Status = "Available";

            // Save changes to the database
            await _context.SaveChangesAsync();

            return Ok(new { message = "Biogas status updated to Available", biogas });
        }

        private bool ProducerExists(int id)
        {
            return _context.Producers.Any(e => e.ProducerId == id);
        }
    }
}
