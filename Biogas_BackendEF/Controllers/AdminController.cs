using Biogas_BackendEF.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Biogas_BackendEF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly BiogasDataBaseContext _context;

        public AdminController(BiogasDataBaseContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get a list of all users
        [HttpGet("Users")]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
        {
            var users = await _context.Users.ToListAsync();
            return Ok(users);
        }

        /// <summary>
        /// Get a list of all waste contributions
        /// </summary>
        [HttpGet("WasteContributions")]
        public async Task<ActionResult<IEnumerable<WasteContributor>>> GetAllWasteContributions()
        {
            var wasteContributions = await _context.WasteContributors.ToListAsync();
            return Ok(wasteContributions);
        }

        /// <summary>
        /// Get a list of all producers
        /// </summary>
        [HttpGet("Producers")]
        public async Task<ActionResult<IEnumerable<Producer>>> GetAllProducers()
        {
            var producers = await _context.Producers.ToListAsync();
            return Ok(producers);
        }

        /// <summary>
        /// Get total count of waste contributions
        /// </summary>
        [HttpGet("TotalWasteContributions")]
        public async Task<ActionResult<int>> GetTotalWasteContributions()
        {
            int totalCount = await _context.WasteContributors.CountAsync();
            return Ok(new { TotalWasteContributions = totalCount });
        }


    }
}
