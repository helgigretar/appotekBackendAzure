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
    public class registerController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public registerController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/members
        [HttpGet]
        public IEnumerable<members> Getmembers()
        {
            return _context.members;
        }

        // GET: api/members/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Getmembers([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var members = await _context.members.FindAsync(id);

            if (members == null)
            {
                return NotFound();
            }

            return Ok(members);
        }

        // PUT: api/members/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Putmembers([FromRoute] int id, [FromBody] members members)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != members.Id)
            {
                return BadRequest();
            }

            _context.Entry(members).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!membersExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/register
        [HttpPost]
        public async Task<IActionResult> Postmembers([FromBody] members members)
        {
            string errors = "";
            if(members.password != members.repeatpassword)
            {
                errors += " The passwords are not the same #";
            }
            if(members.password.Length < 5)
            {
                errors += " Minumum length of the password is 6 letters #";

            }
            //Skoða hvort að það er til usernameið sem er að koma og ef það er til þá skila villu
            var result = _context.members.Where(s => s.username == members.username).ToList();
            if(result.Count > 0)
            {
                errors += " username already exists #";

            }
            if(errors.Length != 0)
            {
                return CreatedAtAction("Getmembers", new { status = errors });

            }
            members = new members
            {
                Id = members.Id,
                name = members.name,
                password = dotNetCoreAzure.Pages.decrypter.cryption.Encrypt(members.password),
                repeatpassword = null,
                username = members.username,
                role = members.role,
            };
            _context.members.Add(members);
            await _context.SaveChangesAsync();
            return CreatedAtAction("Getmembers", new { status = "Success" });
        }
        
        // DELETE: api/members/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletemembers([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var members = await _context.members.FindAsync(id);
            if (members == null)
            {
                return NotFound();
            }

            _context.members.Remove(members);
            await _context.SaveChangesAsync();

            return Ok(members);
        }

        private bool membersExists(int id)
        {
            return _context.members.Any(e => e.Id == id);
        }
    }
}