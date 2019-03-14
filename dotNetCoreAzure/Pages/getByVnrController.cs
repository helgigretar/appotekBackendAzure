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
    public class getByVnrController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public getByVnrController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/getByVnr/5
        [HttpGet("{vnr}")]
        public async Task<IActionResult> Getmeds([FromRoute] int vnr)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var meds = _context.meds.Where(s => s.vnr.ToString() == vnr.ToString()).ToList();
            if (meds.Count>0)
            {
                return Ok(new { meds.Last().id, meds.Last().name, meds.Last().active_ingredient, meds.Last().pharmaceutical_form, meds.Last().strength, meds.Last().atc_code, meds.Last().legal_status, meds.Last().vnr, meds.Last().other_info, meds.Last().marketed, meds.Last().ma_issued  });
            }
            else
            {
                return BadRequest(new { status = "Vnr number does not exist" });
            }
        }
    }
}