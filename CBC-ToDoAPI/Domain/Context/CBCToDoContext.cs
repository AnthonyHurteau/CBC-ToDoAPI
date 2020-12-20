using CBC_ToDoAPI.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CBC_ToDoAPI.Domain.Context
{
    public class CBCToDoContext : DbContext
    {
        public CBCToDoContext(DbContextOptions<CBCToDoContext> options):base(options)
        {
            Database.Migrate();
        }

        public DbSet<ToDoTask> ToDoTask { get; set; }
    }
}
