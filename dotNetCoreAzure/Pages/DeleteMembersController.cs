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
    public class DeleteMembersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DeleteMembersController(ApplicationDbContext context)
        {
            _context = context;
        }



        // POST: api/DeleteMembers
        [HttpPost]
        public async Task<IActionResult> Postmembers([FromBody] members member)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var members = await _context.members.FindAsync(member.Id);
            if (members == null)
            {
                return NotFound(new { status ="there is no user with this id"});
            }
            _context.members.Remove(members);
            await _context.SaveChangesAsync();

            return Ok(new { status = "User has been deleted" });
        }
        
    }
}