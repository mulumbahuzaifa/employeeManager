using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Interfaces;
using backend.Models;
using backend.Data;
using Microsoft.EntityFrameworkCore;

namespace backend.Service
{
    public class EmployeeService : IEmployeeService
    {
        private readonly ApplicationDbContext _context;

        public EmployeeService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Employee> CreateEmployeeAsync(Employee employee)
        {
            if (employee == null)
            {
                throw new ArgumentNullException(nameof(employee));
            }

            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            return employee;
        }

        public async Task<Employee?> GetEmployeeByIdAsync(int id)
        {
            return await _context.Employees.FindAsync(id);
        }

        public async Task<Employee> UpdateEmployeeAsync(Employee employee)
        {

            try
            {
                var existingEmployee = await _context.Employees.Include(e => e.Salary).FirstOrDefaultAsync(e => e.Id == employee.Id);
                if (existingEmployee == null)
                {
                    return null;
                }

                existingEmployee.FirstName = employee.FirstName;
                existingEmployee.LastName = employee.LastName;
                existingEmployee.Email = employee.Email;
                existingEmployee.Phone = employee.Phone;
                existingEmployee.Department = employee.Department;

                // Update salary information
                if (existingEmployee.Salary == null)
                {
                    existingEmployee.Salary = new Salary();
                }
                existingEmployee.Salary.BaseSalary = employee.Salary.BaseSalary;
                existingEmployee.Salary.Bonus = employee.Salary.Bonus;
                existingEmployee.Salary.Date = employee.Salary.Date;

                await _context.SaveChangesAsync();
                return existingEmployee;
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("Failed to update employee", ex);
            }
        }
    }
}