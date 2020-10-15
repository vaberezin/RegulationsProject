using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Regulations.Models;

namespace Regulations.Models.DatabaseContexts
{
    public class RegulationContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Regulation> Regulations { get; set; }

        public RegulationContext(DbContextOptions<RegulationContext> options)
            :base(options)
        {
            Database.EnsureCreated();
        }
    }
}
