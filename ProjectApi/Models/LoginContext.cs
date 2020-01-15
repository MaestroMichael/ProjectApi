using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectApi.Models;

namespace ProjectApi.Models
{
    public class LoginContext : DbContext
    {
        public LoginContext(DbContextOptions<LoginContext> options) : base(options)
        {

        }
        public DbSet<Login> Login { get; set; }
        public DbSet<ProjectApi.Models.Registration> Registration { get; set; }
//        public DbSet<ProjectApi.Models.Registration> Registration { get; set; }
    }
}
