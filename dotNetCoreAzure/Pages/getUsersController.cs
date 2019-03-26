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
    public class getUsersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public getUsersController(ApplicationDbContext context)
        {
            _context = context;
        }
        // POST: api/getUsers --> skilar patients eftrir fr ID
        [HttpPost]
        public async Task<IActionResult> PostdoctorUser([FromBody] doctorUser doctorUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var users = _context.doctorUser.Where(s => s.userID == doctorUser.doctorId).ToList();         
                var members = _context.members.Where(s => s.Id == users.Last().userID).ToList();           
                return Accepted(users);

            }catch{
                return BadRequest(new { status = "villa" });
            }
        }

      
    }
}