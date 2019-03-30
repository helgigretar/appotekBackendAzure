using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using dotNetCoreAzure.Data;
using dotNetCoreAzure.Pages.myObjects;

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
            return _context.meds.Take(500);
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
        // POST: api/getAllMeds --> Finnur lyf eftir leitarvali
        [HttpPost]
        public async Task<IActionResult> Postmeds([FromBody] meds meds)
        {
            String other_info;
            String strength;
            String active_ingredient;
            String pharmaceutical_form;
            String atc_code;
            String ma_issued;
            List<medicine> storage = new List<medicine>();
            var res = _context.meds.Where(s => s.name.Contains(meds.name)).Take(500).ToList();
            if (res.Count > 0)
            {
                foreach (var lyf in res)
                {
                    if (lyf.other_info == null)
                    {
                        other_info = "empty";
                    }
                    else
                    {
                        other_info = lyf.other_info;
                    }
                    if (lyf.strength == null)
                    {
                        strength = "Unknown";
                    }
                    else
                    {
                        strength = lyf.strength;
                    }
                    if (lyf.active_ingredient == null)
                    {
                        active_ingredient = "empty";
                    }
                    else
                    {
                        active_ingredient = lyf.active_ingredient;
                    }
                    if (lyf.pharmaceutical_form == null)
                    {
                        pharmaceutical_form = "empty";
                    }
                    else
                    {
                        pharmaceutical_form = lyf.pharmaceutical_form;
                    }
                    if (lyf.atc_code == null)
                    {
                        atc_code = "empty";
                    }
                    else
                    {
                        atc_code = lyf.atc_code;
                    }
                    if (lyf.ma_issued == null)
                    {
                        ma_issued = "empty";
                    }
                    else
                    {
                        ma_issued = lyf.ma_issued;
                    }
                    medicine medser = new medicine(lyf.id, lyf.name, active_ingredient, pharmaceutical_form, strength,
                                                atc_code, lyf.vnr, other_info, lyf.marketed, ma_issued, lyf.legal_status);
                    storage.Add(medser);
                }
                return Ok(storage);
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