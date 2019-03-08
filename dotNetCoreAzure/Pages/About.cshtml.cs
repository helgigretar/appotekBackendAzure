using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace dotNetCoreAzure.Pages
{
    public class AboutModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;

        public AboutModel(UserManager<IdentityUser> userManager)

        {
            _userManager = userManager;

        }
        public String Message { get; set; }
        public async void OnGetAsync()
        {
            var user = new IdentityUser
            {
                UserName = "helgi Grétar Gunnars tóks ?",
                PasswordHash ="Pass"
            };
            var result = await _userManager.CreateAsync(user, "pass");
            Message = "Your application description page.";
        }
    }
}
