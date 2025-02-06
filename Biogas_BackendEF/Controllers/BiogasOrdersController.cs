using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Biogas_BackendEF.Models;
using Razorpay.Api;

namespace Biogas_BackendEF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BiogasOrdersController : ControllerBase
    {
        private readonly BiogasDataBaseContext _context;

        public BiogasOrdersController(BiogasDataBaseContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Biogas>>> GetAvailableBiogas()
        {
            var biogasList = await _context.Biogas.Where(b => b.Quantity > 0).ToListAsync();
            if (!biogasList.Any())
            {
                return NotFound("No biogas available.");
            }

            return Ok(biogasList);
        }



        // POST: api/BiogasOrders/BuyBiogas
        [HttpPost("BuyBiogas")]
        public async Task<IActionResult> BuyBiogas(int userId, int biogasId, double quantity)
        {
            // Step 1: Check if the user is a customer
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UId== userId);
            if (user == null || user.Role != "customer")
            {
                return BadRequest("Only customers can buy biogas.");
            }

            // Step 2: Check if biogas is available
            var biogas = await _context.Biogas.FirstOrDefaultAsync(b => b.BiogasId == biogasId);
            if (biogas == null || biogas.Quantity < quantity)
            {
                return BadRequest("Not enough biogas available.");
            }

            // Step 3: Create an order
            var newOrder = new Models.Order
            {
                UserId = userId,
                BiogasId = biogasId,
                Quantity = quantity,
                Status = "Pending", // Initial status
            };

            // Update biogas quantity
            biogas.Quantity -= quantity;

            // Add the order to the database
            _context.BiogasOrders.Add(newOrder);
            await _context.SaveChangesAsync();

            // Step 4: Initialize Razorpay Payment (create an order in Razorpay)
            try
            {
                var razorpayClient = new RazorpayClient("YOUR_KEY_ID", "YOUR_KEY_SECRET");

                var options = new Dictionary<string, object>
                {
                    { "amount", biogas.Price * quantity * 100 }, // amount in paise (1 INR = 100 paise)
                    { "currency", "INR" },
                    { "receipt", newOrder.OrderId.ToString() },
                    { "payment_capture", 1 } // 1 for automatic capture, 0 for manual capture
                };

                var razorpayOrder = razorpayClient.Order.Create(options);
                string razorpayOrderId = razorpayOrder["id"].ToString();

                // Save the Razorpay order ID and associate it with the order in your database
                newOrder.TransactionId = razorpayOrderId;

                // Update the order with the Razorpay transaction ID
                _context.Entry(newOrder).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return Ok(new
                {
                    Message = "Order created successfully",
                    OrderId = newOrder.OrderId,
                    RazorpayOrderId = razorpayOrderId
                });
            }
            catch (Exception ex)
            {
                return BadRequest($"Razorpay API error: {ex.Message}");
            }
        }
    }
}
