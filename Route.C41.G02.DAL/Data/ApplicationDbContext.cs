using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Route.C41.G02.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace Route.C41.G02.DAL.Data
{
    public class ApplicationDbContext: IdentityDbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options):base(options)
        {

        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //=> optionsBuilder.UseSqlServer("Server = LAPTOP-ASFDTF01\\MSSQLSERVERR; Database = RouteMVCProject;TrustedServerCertificate = True");


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
        public DbSet<Department> Departments{ get; set; }
        public DbSet<Employee>employees  { get; set; }


    }
}
