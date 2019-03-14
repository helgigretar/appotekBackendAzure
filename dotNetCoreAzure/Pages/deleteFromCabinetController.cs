using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using dotNetCoreAzure.Data;

namespace dotNetCoreAzure.Pages
{
    [Route("api/[controller]")]
    [ApiController]
    public class deleteFromCabinetController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public deleteFromCabinetController(ApplicationDbContext context)
        {
            _context = context;
        }

       

        // POST: api/deleteFromCabinet
        [HttpPost]
        public async Task<IActionResult> Postcabinet([FromBody] cabinet cabinet)
        {
            var result = _context.cabinet.Where(S => S.userID == cabinet.userID && S.medicineID == cabinet.medicineID).ToList();
            try
            {
                var temp = _context.cabinet.Remove(result.Last());
                await _context.SaveChangesAsync();
                return Ok(new { status = "Medicine has been deleted" });
            }
            catch
            {
                return BadRequest(new { status = "Bad error" });
            }



        }
        private bool cabinetExists(int id)
        {
            return _context.cabinet.Any(e => e.Id == id);
        }
    }
}