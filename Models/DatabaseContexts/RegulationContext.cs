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
        public DbSet<Role> Roles { get; set; }
        

        public RegulationContext(DbContextOptions<RegulationContext> options)
            :base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder){ 
            //creating Administrator Account
            string AdministratorLogin = "Admin";
            string AdministratorPassword = "Passme";
            //creating role names
            string AdministratorRole = "admin";
            string UserRole = "user";
            //creating roles
            Role admin = new Role{Id =1, Name = AdministratorRole};
            Role user = new Role{Id = 2, Name = UserRole};
            //Creating 'Admin' User
            User adminUser = new User{Id = 1, Email = AdministratorLogin, Password = AdministratorPassword, RoleId = admin.Id};
            //Adding Roles and Adminuser into Database
            modelBuilder.Entity<Role>().HasData(new Role[]{admin, user});
            modelBuilder.Entity<User>().HasData(new User[]{adminUser});
            base.OnModelCreating(modelBuilder);
        }
    }   

}
