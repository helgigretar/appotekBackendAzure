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
    public class cabinetsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public cabinetsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/cabinets
        [HttpGet]
        public IEnumerable<cabinet> Getcabinet()
        {
            return _context.cabinet;
        }

        // GET: api/cabinets/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Getcabinet([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var cabinet = await _context.cabinet.FindAsync(id);

            if (cabinet == null)
            {
                return NotFound();
            }

            return Ok(cabinet);
        }

        // PUT: api/cabinets/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Putcabinet([FromRoute] int id, [FromBody] cabinet cabinet)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cabinet.Id)
            {
                return BadRequest();
            }

            _context.Entry(cabinet).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!cabinetExists(id))
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
        // Tekur inn id fyrir medicine og user og setur það í töfluna 
        // POST: api/cabinets
        [HttpPost]
        public async Task<IActionResult> Postcabinet([FromBody] cabinet cabinet)
        {
            bool doesExist = false;
            var resUser = _context.members.Where(s => s.Id == cabinet.userID).ToList();
            var resMedicine = _context.meds.Where(s => s.id == cabinet.medicineID).ToList();
            var resCabinet = _context.cabinet.Where(s => s.userID == cabinet.userID && s.medicineID == cabinet.medicineID).ToList();
            if(resUser.Count == 0 || resMedicine.Count == 0)
            {
                return BadRequest(new { status = "Error has occured make sure ID for user and medicine is available" });
            }
            else
            {
                _context.cabinet.Add(cabinet);
                await _context.SaveChangesAsync();
                return Ok(new{status="medicine has been added to your account" });
            }
        }

        // DELETE: api/cabinets/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletecabinet([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var cabinet = await _context.cabinet.FindAsync(id);
            if (cabinet == null)
            {
                return NotFound();
            }

            _context.cabinet.Remove(cabinet);
            await _context.SaveChangesAsync();

            return Ok(cabinet);
        }

        private bool cabinetExists(int id)
        {
            return _context.cabinet.Any(e => e.Id == id);
        }
    }
}