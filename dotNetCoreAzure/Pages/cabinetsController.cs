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
            List<medicine> storage = new List<medicine>();
            
            var result = _context.cabinet.Where(s => s.userID == Int32.Parse(id.ToString())).ToList();
            if(result.Count > 0)
            {
                String other_info;
                String strength;
                String active_ingredient;
                String pharmaceutical_form;
                String atc_code;
                String ma_issued;
                foreach (var item in result)
                {
                    var lyf = _context.meds.SingleOrDefault(s => s.id == item.medicineID);
                    if(lyf.other_info == null)
                    {
                        other_info = "empty";
                    }
                    else
                    {
                        other_info = lyf.other_info;
                    }
                    if(lyf.strength == null)
                    {
                        strength = "Unknown";
                    }
                    else
                    {
                        strength = lyf.strength;
                    }
                    if(lyf.active_ingredient == null)
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
                    if(lyf.ma_issued == null)
                    {
                        ma_issued = "empty";
                    }
                    else
                    {
                        ma_issued = lyf.ma_issued;
                    }
                    medicine meds = new medicine(lyf.id,lyf.name, active_ingredient, pharmaceutical_form, strength,
                                                atc_code,lyf.vnr, other_info, lyf.marketed,ma_issued,lyf.legal_status );
                    storage.Add(meds);
                }
                return Ok(storage);
            }
            else
            {
                return BadRequest(new { status = "You have no medicine in your cabines" });
            }
        }

        
        // Tekur inn id fyrir medicine og user og setur það í töfluna hjá patient
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