using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
// using backend.Dtos.Salary;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Dtos.Employee
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
        public decimal? BaseSalary { get; set; } // Assuming BaseSalary is nullable
        public decimal? Bonus { get; set; } // Assuming Bonus is nullable
        public DateTime Date { get; set; } = DateTime.Now;// Assuming Date is nullable
    }
}