﻿using System;
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
    public class getAllPatients : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public getAllPatients(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/getAllPatietsotMine
        [HttpGet]
        public async Task<IActionResult> getAllPatientsNotMine()
        {
            {
                var patients = _context.members.Where(s => s.role == "false").ToList();
                List<pateintsForDoctors> finalPatients = new List<pateintsForDoctors>();
                foreach (var data in patients)
                {
                    finalPatients.Add(new pateintsForDoctors { id = data.Id, username = data.username, name = data.name });

                }

                return Ok(finalPatients);
            }
        }

        // GET: api/getAllPatientsNotMine/5
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

        // PUT: api/getAllPatientsNotMine/5
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

        // POST: api/getAllPatientsNotMine
        [HttpPost]
        public async Task<IActionResult> Postmembers([FromBody] members members)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.members.Add(members);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Getmembers", new { id = members.Id }, members);
        }

        // DELETE: api/getAllPatientsNotMine/5
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