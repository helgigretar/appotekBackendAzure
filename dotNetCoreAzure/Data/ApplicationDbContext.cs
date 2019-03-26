using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using dotNetCoreAzure.Data;

namespace dotNetCoreAzure.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<members> members { get; set; }
        public DbSet<meds> meds { get; set; }
        public DbSet<cabinet> cabinet { get; set; }
        public DbSet<dotNetCoreAzure.Data.doctorUser> doctorUser { get; set; }
    }
}
