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
    public class doctorUsersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public doctorUsersController(ApplicationDbContext context)
        {
            _context = context;
        }

        public string username { get; private set; }
        public string name { get; private set; }

        //Id is for the doctor and it gives back all of his patients
        // Get: api/doctorUsers/ID
        [HttpGet("{id}")]
        public async Task<IActionResult> Getmeds([FromRoute] int id)
        {
            var doctors = _context.members.Where(s => s.role == "true" && s.Id == id).ToList();
            if(doctors.Count == 0)
            {
                return BadRequest(new { status = "this Id is not a doctor" });
            }
            var patients = _context.doctorUser.Where(s => s.doctorId == id).ToList();
            if(patients.Count == 0)
            {
                return BadRequest(new { status = "this doctor has no patients" });
            }
            // Querie for inner join. the only 
            var patints = _context.doctorUser.Where(s => s.doctorId == id).ToList();
            var membs = _context.members.Where(d => d.Id >= 0).ToList();
            List<pateintsForDoctors> finalPatients = new List<pateintsForDoctors>();
            foreach(var item in patients)
            {
                foreach(var data in membs)
                {
                    if(item.userID == data.Id)
                    {
                        finalPatients.Add(new pateintsForDoctors { id = data.Id, username = data.username, name = data.name });
                    }
                }
            }
            return Ok(finalPatients);
        }
        // POST: api/doctorUsers
        // Setur user á vissan lækni eftir ID 
        [HttpPost]
        public async Task<IActionResult> PostdoctorUser([FromBody] doctorUser doctorUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var doctors = _context.members.Where(s => s.role == "true" && s.Id == doctorUser.doctorId).ToList();
            if (doctors.Count == 0)
            {
                return BadRequest(new { status = "Doctor does not exist" });
            }
            var patients = _context.members.Where(s => s.Id == doctorUser.userID).ToList();
            if (patients.Count == 0)
            {
                return BadRequest(new { status = "Patients does not exist" });
            }
            var doctorUsers = _context.doctorUser.Where(s => s.doctorId == doctorUser.doctorId && s.userID == doctorUser.userID).ToList();
            if(doctorUsers.Count != 0)
            {
                return BadRequest(new { status = "Doctor already has this patient" });
            }
            // Hér þarf að skoða hvort DoctorId er læknir og hvort userId er patient. 
            // Þegar það er rétt þá má sjúklingurinn fara í baskett hjá lækninum.


            _context.doctorUser.Add(doctorUser);
            await _context.SaveChangesAsync();

            return Accepted(new { status = "User has been added to the doctor home area" });
        }
    }
}