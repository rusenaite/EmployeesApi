using EmployeeApi.Api.Entities;
using EmployeeApi.Infrastructure;
using EmployeeApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeApi.Controllers
{
    [Route("api/employees")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly EmployeesDbContext _context;

        public EmployeesController(EmployeesDbContext context)
        {
            _context = context;
            DataSeeder.SeedData(_context);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<EmployeeDto>> GetAllEmployees()
        {
            var employees = _context.Employees
                .Select(employee => employee.AsDto(_context.Roles.Single(x => x.Id == employee.RoleId)))
                .ToList();
            
            return Ok(employees);
        }

        [HttpGet("/{position}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<EmployeeDto>> GetEmployeesByPosition(string position)
        {
            var employees = _context.Employees
                .Select(employee => employee.AsDto(_context.Roles.Single(x => x.Id == employee.RoleId)))
                .ToList();

            var employeesInRole = employees.Where(x => x.Role.Position.ToString() == position).ToList();

            return employeesInRole.Count is not 0 ? Ok(employeesInRole) : NotFound();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult AddNewEmployee(CreateEmployeeDto employeeDto)
        {
            var employee = new Employee()
            {
                Id = Guid.NewGuid(),
                FirstName = employeeDto.FirstName,
                LastName = employeeDto.LastName,
                BirthDate = employeeDto.BirthDate,
                HomeAddress = employeeDto.HomeAddress,
                CurrentSalary = employeeDto.CurrentSalary,
                RoleId = _context.Roles.ToList().FirstOrDefault(x => x.Position.ToString() == employeeDto.PositionName)?.Id ?? new Role()
                {
                    Id = Guid.NewGuid(),
                    Position = Position.NotDefinedYet.ToString(),
                    HoursPerWeek = 40,
                    Description = "Role is not clear at this moment."
                }.Id
            };

            _context.Employees.Add(employee);
            _context.SaveChanges();

            return CreatedAtAction(nameof(AddNewEmployee), employee.Id);
        }

        [HttpPut("/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateEmployee(Guid id, UpdateEmployeeDto updatedEmployee)
        {
            var employee = _context.Employees.SingleOrDefault(x => x.Id == id);

            if(employee is null)
            {
                return NotFound("Such employee does not exist.");
            }

            employee.FirstName = updatedEmployee.FirstName;
            employee.LastName = updatedEmployee.LastName;
            employee.BirthDate = updatedEmployee.BirthDate;
            employee.CurrentSalary = updatedEmployee.CurrentSalary;
            employee.HomeAddress = updatedEmployee.HomeAddress;
            employee.RoleId = updatedEmployee.RoleId;

            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteEmployee(Guid id)
        {
            var employee = _context.Employees.SingleOrDefault(x => x.Id == id);

            if (employee != null)
            {
                _context.Employees.Remove(employee);
                _context.SaveChanges();

                return NoContent();
            }
            
            return NotFound();
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult DeleteAllEmployees()
        {
            _context.Employees.ExecuteDelete();
            _context.SaveChanges();

            return NoContent();
        }
    }
}
