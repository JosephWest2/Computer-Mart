using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Computer_Mart.Models;
using Computer_Mart.Models.Auth;

namespace Computer_Mart.Data
{
    public class Computer_MartContext : DbContext
    {
        public Computer_MartContext (DbContextOptions<Computer_MartContext> options)
            : base(options)
        {
        }

        public DbSet<CPU> CPU { get; set; }

        public DbSet<GPU> GPU { get; set; }

        public DbSet<RAM> RAM { get; set; }

        public DbSet<SSD> SSD { get; set; }

        public DbSet<Computer> Computer { get; set; }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Username = "Admin",
                    PasswordHash = Statics.AuthenticateUser.CreatePasswordHash("Admin1234*"),
                    Admin = true,
                    Id = -1
                });

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();
        }
    }
}
