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
        //Get the cabinet from the user of the id
        // GET: api/cabinets/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Getcabinet([FromRoute] int id)
        {
            var result = _context.cabinet.Where(s => s.userID == Int32.Parse(id.ToString())).ToList();
            if(result.Count > 0)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(new { status = "You have no medicine in your cabines" });
            }
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
            if (resCabinet.Count > 0)
            {
                return BadRequest(new { status = "You have this medicine in your cabinet" });
            }
            else
            {
                _context.cabinet.Add(cabinet);
                await _context.SaveChangesAsync();
                return Ok(new{status="medicine has been added to your account" });
            }
        }
    }
}