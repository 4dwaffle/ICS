﻿using Microsoft.EntityFrameworkCore;
using Seminar_CSharp.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seminar_CSharp
{
    internal class ApplicationContext : DbContext
    {
        public DbSet<Project> Projects { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Activity> Activities { get; set; }
        
        public ApplicationContext()
        {
            Database.EnsureDeleted(); //For 1st create. Then Delete this. I leave this for 1st faze,after ill delete this
           Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=ICS;Trusted_Connection=True;");
        }
       
    }
}
