using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


namespace backend.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
            
        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Salary> Salaries { get; set; }

        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
             modelBuilder.Entity<Salary>()
                .Property(s => s.Date)
                .HasColumnType("datetime"); 
                // Specify datetime or datetime2 as per your SQL Server version

            modelBuilder.Entity<Salary>()
                .Property(s => s.BaseSalary)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Salary>()
                .Property(s => s.Bonus)
                .HasColumnType("decimal(18,2)");
                base.OnModelCreating(modelBuilder);

            // modelBuilder.Entity<Employee>().HasData(
            //     new Employee { Id = 1, FirstName = "John", LastName = "Doe", Email = "john.doe@example.com", Phone = "123-456-7890", Department = "Engineering" },
            //     new Employee { Id = 2, FirstName = "Jane", LastName = "Smith", Email = "jane.smith@example.com", Phone = "234-567-8901", Department = "Marketing" },
            //     new Employee { Id = 3, FirstName = "Alice", LastName = "Johnson", Email = "alice.johnson@example.com", Phone = "345-678-9012", Department = "Sales" },
            //     new Employee { Id = 4, FirstName = "Bob", LastName = "Brown", Email = "bob.brown@example.com", Phone = "456-789-0123", Department = "HR" }
            // );

            // modelBuilder.Entity<Salary>().HasData(
            //     new Salary { Id = 1, EmployeeId = 1, BaseSalary = 60000.00m, Bonus = 5000.00m, Date = new DateTime(2023, 1, 1) },
            //     new Salary { Id = 2, EmployeeId = 2, BaseSalary = 55000.00m, Bonus = 3000.00m, Date = new DateTime(2023, 1, 1) },
            //     new Salary { Id = 3, EmployeeId = 3, BaseSalary = 50000.00m, Bonus = 2000.00m, Date = new DateTime(2023, 1, 1) },
            //     new Salary { Id = 4, EmployeeId = 4, BaseSalary = 48000.00m, Bonus = 1500.00m, Date = new DateTime(2023, 1, 1) }
            // );
             List<IdentityRole> roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "USER"
                },
            };
            modelBuilder.Entity<IdentityRole>().HasData(roles);
        }
    }
}