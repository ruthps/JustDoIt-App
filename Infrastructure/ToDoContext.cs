using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using JustDoIt.Models;

namespace JustDoIt.Infrastructure
{
    public class ToDoContext    : DbContext
    {
        public ToDoContext(DbContextOptions<ToDoContext> options) 
            : base(options)
        {

        }
        public DbSet<ToDoList> JustDoIt { get; set; }
    }
}
