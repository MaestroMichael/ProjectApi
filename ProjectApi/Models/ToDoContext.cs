using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectApi.Models;

namespace ProjectApi.Models
{
    public class ToDoContext:DbContext
    {
        public ToDoContext(DbContextOptions<ToDoContext> options):base(options)
        {

        }
        public DbSet<TodoItem> ToDoItems { get; set; }
        public DbSet<ProjectApi.Models.Registration> Registration { get; set; }
    }
}
