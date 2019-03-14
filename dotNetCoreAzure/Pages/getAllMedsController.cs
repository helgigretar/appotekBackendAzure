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
    //SKilar öllum lyfjum 
    public class getAllMedsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public getAllMedsController(ApplicationDbContext context)
        {
            _context = context;
        }
        //Nær í öll lyf
        // GET: api/getAllMeds
        [HttpGet]
        public IEnumerable<meds> Getmeds()
        {
            return _context.meds.Take(10);
        }
        //Nær í lyf af ID
        // GET: api/getAllMeds/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Getmeds([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var meds = await _context.meds.FindAsync(id);


            if (meds == null)
            {
                return NotFound();
            }

            return Ok(meds);
        }
        // POST: api/getAllMeds
        [HttpPost]
        public async Task<IActionResult> Postmeds([FromBody] meds meds)
        {
            var res = _context.meds.Where(s => s.name.Contains(meds.name)).Take(10).ToList();
            if (res.Count > 0)
            {
                return Ok(new { res});
            }
            else
            {
               return BadRequest(new { status = "There is no medicine with that name" });
            }

        }
        private bool medsExists(int id)
        {
            return _context.meds.Any(e => e.id == id);
        }
    }
}