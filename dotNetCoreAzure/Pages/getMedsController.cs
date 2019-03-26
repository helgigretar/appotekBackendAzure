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
    public class getMedsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public getMedsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/getMeds
        [HttpGet]
        public IEnumerable<meds> Getmeds()
        {
            return _context.meds;
        }

        // GET: api/getMeds/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Getmeds([FromRoute] String vpnr)
        {
            var meds = await _context.meds.FindAsync(vpnr);

            if (meds == null)
            {
                return NotFound();
            }

            return Ok(meds);
        }

        // PUT: api/getMeds/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Putmeds([FromRoute] int id, [FromBody] meds meds)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != meds.id)
            {
                return BadRequest();
            }

            _context.Entry(meds).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!medsExists(id))
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
       

        private bool medsExists(int id)
        {
            return _context.meds.Any(e => e.id == id);
        }
    }
}