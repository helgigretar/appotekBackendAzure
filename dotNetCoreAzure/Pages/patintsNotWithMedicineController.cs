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
    public class patintsNotWithMedicineController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public patintsNotWithMedicineController(ApplicationDbContext context)
        {
            _context = context;
        }

        

        // POST: api/patintsNotWithMedicine
        [HttpPost]
        public async Task<IActionResult> PostpateintsForDoctors([FromBody] doctorPatientsAdd doctorPatientsAdd)
        {
            //gives me all of the patientsID for this user
            var usersnotWithDoc = _context.doctorUser.Where(s =>s.doctorId.ToString() == doctorPatientsAdd.doctorId).ToList();
            if (usersnotWithDoc.Count == 0)
            {
                return BadRequest(new { status = "this doctor has no patients" });
            }
            // check if these patients have  this medicine in there cabines
           
            List<members> membersDoctor = new List<members>();
            foreach(var item in usersnotWithDoc)
            {
                var temp = _context.cabinet.Where(s =>
                            s.userID == item.userID &&
                            s.medicineID.ToString() == doctorPatientsAdd.medicineId
                            ).ToList();
                if (temp.Count == 0)
                {
                    var userTemp = _context.members.Where(s => s.Id == item.userID).ToList();
                    membersDoctor.Add(new members{
                        Id =userTemp.Last().Id,
                        name = userTemp.Last().name,
                        socialID = userTemp.Last().socialID,

                    });
                }
                //Now I have all of the users from this doctor using this medication but I only have the ID
            }
            if(membersDoctor.Count == 0)
            {
                return BadRequest(new { status = "All of the patient has this medicine" });
            }
            else
            {
                return Ok(membersDoctor);
            }
        }

      
    }
}