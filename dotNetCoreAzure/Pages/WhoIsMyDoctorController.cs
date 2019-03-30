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
    public class WhoIsMyDoctorController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public WhoIsMyDoctorController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/WhoIsMyDoctor/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetdoctorUser([FromRoute] int id)
        {
            var doctorName = _context.members.Where(s => s.Id == id && s.role == "true").ToList();
            if (doctorName.Count > 0)
            {
                String temp = "Welcome Dr " + doctorName.Last().name;
                return Ok(new { name = temp });
            }
            var doctorId = _context.doctorUser.Where(S => S.userID == id).ToList();
            if (doctorId.Count == 0)
            {
                return Accepted(new { name = "No doctor assigned to the account" });
            }
            var doctor = _context.members.Where(d => d.Id == doctorId.Last().doctorId).ToList();
            String tempDr = "Your Dr name is " + doctor.Last().name;
            return Accepted(new { name = tempDr});
        }
    }
}