﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using dotNetCoreAzure.Data;
using System.Net.Mail;

namespace dotNetCoreAzure.Pages
{
    [Route("api/[controller]")]
    [ApiController]
    public class loginController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public loginController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/login
        [HttpGet]
        public IEnumerable<members> Getmembers()
        {
            return _context.members;
        }

        // GET: api/login/5
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

        // PUT: api/login/5
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

        // POST: api/login
        [HttpPost]
        public async Task<IActionResult> Postmembers([FromBody] members members)
        {
            string username = format(members.username);
            string password = format(members.password);
            //Skoða hvort að það er til usernameið og rétt paswword
            var result = _context.members.Where(s => s.username == username && dotNetCoreAzure.Pages.decrypter.cryption.Decrypt(s.password) == password).ToList();
            if (result.Count > 0)
            {
                return Ok(new {result.Last().Id, result.Last().username, result.Last().name});
            }
            else
            {
                return BadRequest(new { status = "Wrong credentials" });
            }            
        }
        public string format(string temp)
        {
            temp = temp.Replace("[", "");
            temp = temp.Replace("]", "");
            return temp;
        }
        // DELETE: api/login/5
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