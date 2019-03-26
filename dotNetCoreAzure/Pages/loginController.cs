using System;
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

        // GET: api/login ÞETTA ROUTE ER ÓLÖGLEGT OG ÉG ÞARF AÐ EYÐA ÞVÍ
        [HttpGet]
        public IEnumerable<members> Getmembers()
        {
            return _context.members;
        }

        // POST: api/login("umsername","password") __> SKilaer nafni
        [HttpPost]
        public async Task<IActionResult> Postmembers([FromBody] members members)
        {
            string username = format(members.username);
            string password = format(members.password);
            //Skoða hvort að það er til usernameið og rétt paswword
            var result = _context.members.Where(s => s.username == username && dotNetCoreAzure.Pages.decrypter.cryption.Decrypt(s.password) == password).ToList();
            if (result.Count > 0)
            {
                return Ok(new { result.Last().Id, result.Last().username, result.Last().name, result.Last().role });
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
        
    }
}