using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Dtos.Employee;
using backend.Models;

namespace backend.Mappers
{
    public static class EmployeeMappers
    {
        public static EmployeeDto ToEmployeeDto(this Employee EmployeeModel){
            return new EmployeeDto
            {
                Id = EmployeeModel.Id,
                FirstName = EmployeeModel.FirstName,
                LastName = EmployeeModel.LastName,
                Email = EmployeeModel.Email,
                Phone = EmployeeModel.Phone,
                Department = EmployeeModel.Department,
                BaseSalary = EmployeeModel.Salary?.BaseSalary ?? 0,
                Bonus = EmployeeModel.Salary?.Bonus ?? 0,
                Date = EmployeeModel.Salary?.Date ?? DateTime.Now
            };
        }
        public static Employee ToEmployeeModel(this CreateEmployeeRequestDto EmployeeDto){
            return new Employee
            {
                FirstName = EmployeeDto.FirstName,
                LastName = EmployeeDto.LastName,
                Email = EmployeeDto.Email,
                Phone = EmployeeDto.Phone,
                Department = EmployeeDto.Department,
                Salary = new Salary // Assuming Salary is a related entity or complex type
                {
                    BaseSalary = EmployeeDto.BaseSalary ?? 0,
                    Bonus = EmployeeDto.Bonus ?? 0,
                    Date = EmployeeDto.Date
                }
            };
        }
        public static Employee ToEmployeeModel2(this UpdateEmployeeRequestDto dto, Employee existingEmployee)
        {
            existingEmployee.FirstName = dto.FirstName;
            existingEmployee.LastName = dto.LastName;
            existingEmployee.Email = dto.Email;
            existingEmployee.Phone = dto.Phone;
            existingEmployee.Department = dto.Department;
             if (existingEmployee.Salary == null)
            {
                existingEmployee.Salary = new Salary();
            }
            existingEmployee.Salary.BaseSalary = dto.BaseSalary;
            existingEmployee.Salary.Bonus = dto.Bonus;
            existingEmployee.Salary.Date = dto.Date;

            // if (existingEmployee.Salary != null)
            // {
            //     // existingEmployee.Salary = new Salary();
            //     existingEmployee.Salary.BaseSalary = dto.BaseSalary ?? 10; // Provide a default value if null
            //     existingEmployee.Salary.Bonus = dto.Bonus ?? 10; // Provide a default value if null
            //     existingEmployee.Salary.Date = dto.Date; // Provide a default value if null
            // }else
            // {
            //     existingEmployee.Salary = new Salary
            //     {
            //         BaseSalary = employeeDto.BaseSalary ?? 0,
            //         Bonus = employeeDto.Bonus ?? 0
            //     };
            // }

            return existingEmployee;
        }
    }
}