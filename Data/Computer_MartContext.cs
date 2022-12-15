using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Computer_Mart.Models;

namespace Computer_Mart.Data
{
    public class Computer_MartContext : DbContext
    {
        public Computer_MartContext (DbContextOptions<Computer_MartContext> options)
            : base(options)
        {
        }

        public DbSet<Computer_Mart.Models.CPU> CPU { get; set; } = default!;

        public DbSet<Computer_Mart.Models.GPU> GPU { get; set; } = default!;

        public DbSet<Computer_Mart.Models.RAM> RAM { get; set; } = default!;

        public DbSet<Computer_Mart.Models.SSD> SSD { get; set; } = default!;

        public DbSet<Computer_Mart.Models.Computer> Computer { get; set; } = default!;
    }
}
