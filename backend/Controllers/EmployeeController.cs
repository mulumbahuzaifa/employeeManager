using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using backend.Data;
using backend.Mappers;
using backend.Service;
using backend.Interfaces;
using backend.Dtos.Employee;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers
{
    [Route("api/employee")]
    [ApiController]

    public class EmployeeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IEmployeeService _employeeService;

        public EmployeeController(ApplicationDbContext context, IEmployeeService employeeService)
        {
            _context = context;
            _employeeService = employeeService;
        }
        [HttpGet]
        public IActionResult GetAll(){
            var employees = _context.Employees.Include(e => e.Salary).ToList()
            .Select(s => s.ToEmployeeDto());
            return Ok(employees);
        }
        // public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        // {
        //     return await _context.Employees.ToListAsync();
        // }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var employee = await _context.Employees.Include(e => e.Salary).FirstOrDefaultAsync(e => e.Id == id);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee.ToEmployeeDto());
        }
        // public IActionResult GetById([FromRoute] int id){
        //     var employee = _context.Employees.Include(e => e.Salary).FirstOrDefaultAsync(e => e.Id == id);
        //     if(employee == null){
        //         return NotFound();
        //     }
        //     return Ok(employee.ToEmployeeDto());
        // }
        // public async Task<ActionResult<Employee>> GetEmployee(int id)
        // {
        //     var employee = await _context.Employees.FindAsync(id);
        //     if (employee == null)
        //     {
        //         return NotFound();
        //     }
        //     return employee;
        // }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateEmployeeRequestDto employeeDto){
            if (employeeDto == null)
            {
                return BadRequest();
            }

            var employee = employeeDto.ToEmployeeModel();
            var createdEmployee = await _employeeService.CreateEmployeeAsync(employee);
            if (createdEmployee == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating new employee record");
            }

            var empDto = createdEmployee.ToEmployeeDto();
            // _context.Employees.Add(employee);
            // _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = empDto.Id}, empDto);
            // return Ok(employee.ToEmployeeDto());
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateEmployeeRequestDto employeeDto){
            if (employeeDto == null)
            {
                return BadRequest();
            }
            var existingEmployee = await _employeeService.GetEmployeeByIdAsync(id);
            if(existingEmployee == null){
                return NotFound();
            }
            existingEmployee  = employeeDto.ToEmployeeModel2(existingEmployee);

            var updatedEmployee = await _employeeService.UpdateEmployeeAsync(existingEmployee);

            if (updatedEmployee == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating employee record");
            }
            // _context.SaveChanges();
            // Return updated employee DTO
            var empDto = updatedEmployee.ToEmployeeDto();
            return Ok(empDto);
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete([FromRoute] int id){
            var employee = _context.Employees.Include(e => e.Salary).FirstOrDefault(e => e.Id == id);
            if(employee == null){
                return NotFound();
            }
            _context.Employees.Remove(employee);
            _context.SaveChanges();
            return Ok(employee.ToEmployeeDto());
        }

        [HttpGet("export")]
        public async Task<IActionResult> ExportEmployees()
        {
            var employees = await _context.Employees.Include(e => e.Salary).ToListAsync();

            var csv = new StringBuilder();
            csv.AppendLine("Id,FirstName,LastName,Email,Phone,Department,BaseSalary,Bonus,Date");

            foreach (var employee in employees)
            {
                csv.AppendLine($"{employee.Id},{employee.FirstName},{employee.LastName},{employee.Email},{employee.Phone},{employee.Department},{employee.Salary?.BaseSalary},{employee.Salary?.Bonus},{employee.Salary?.Date}");
            }

            var csvBytes = Encoding.UTF8.GetBytes(csv.ToString());
            return File(csvBytes, "text/csv", "employees.csv");
        }

        // [HttpGet("export")]
        // public IActionResult ExportEmployees()
        // {
        //     var employees = _context.Employees.Include(e => e.Salary).ToList()
        //     .Select(s => s.ToEmployeeDto());;

        //     if(employees == null){
        //         return NotFound();
        //     }
        //      var csv = new StringBuilder();
        //     csv.AppendLine("Id,FirstName,LastName,Email,Phone,Department,BaseSalary,Bonus,Date");

        //     foreach (var employee in employees)
        //     {
        //         csv.AppendLine($"{employee.Id},{employee.FirstName},{employee.LastName},{employee.Email},{employee.Phone},{employee.Department},{employee.Salary?.BaseSalary},{employee.Salary?.Bonus},{employee.Salary?.Date}");
        //     }

        //     var csvBytes = Encoding.UTF8.GetBytes(csv.ToString());
        //     return File(csvBytes, "text/csv", "employees.csv");
        // }
    }
}