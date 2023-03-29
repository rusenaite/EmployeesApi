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
        //private readonly EmployeesDbContext _context;
        private readonly List<Employee> _employees;
        private readonly List<Role> _roles;

        public EmployeesController()
        {
            //_context = context;
            //DataSeeder.SeedData(_context);
            if(_roles == null && _employees == null) {
                _roles = DataSeeder.SeedRoles();
                _employees = DataSeeder.SeedEmployees(_roles);
            }
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<EmployeeDto>> GetAllEmployees()
        {
            //var employees = _context.Employees
            //    .Select(employee => employee.AsDto(_context.Roles.Single(x => x.Id == employee.RoleId)))
            //    .ToList();

            var employees = _employees
                .Select(employee => employee.AsDto(_roles.Single(x => x.Id == employee.RoleId)))
                .ToList();

            return Ok(employees);
        }

        [HttpGet("/{position}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<EmployeeDto>> GetEmployeesByPosition(string position)
        {
            //var employees = _context.Employees
            //    .Select(employee => employee.AsDto(_context.Roles.Single(x => x.Id == employee.RoleId)))
            //    .ToList();

            var employees = _employees
                .Select(employee => employee.AsDto(_roles.Single(x => x.Id == employee.RoleId)))
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
                //RoleId = _context.Roles.ToList().FirstOrDefault(x => x.Position.ToString() == employeeDto.PositionName)?.Id ?? new Role()
                RoleId = _roles.FirstOrDefault(x => x.Position.ToString() == employeeDto.PositionName)?.Id ?? new Role()
                {
                    Id = Guid.NewGuid(),
                    Position = Position.NotDefinedYet.ToString(),
                    HoursPerWeek = 40,
                    Description = "Role is not clear at this moment."
                }.Id
            };

            //_context.Employees.Add(employee);
            //_context.SaveChanges();

            return CreatedAtAction(nameof(AddNewEmployee), employee.Id);
        }

        [HttpPut("/{employeeId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateEmployee(Guid employeeId, UpdateEmployeeDto updatedEmployee)
        {
            //var employee = _context.Employees.SingleOrDefault(x => x.Id == id);
            var employee = _employees.SingleOrDefault(x => x.Id == employeeId);

            if(employee is null)
            {
                return NotFound("Such employee does not exist.");
            }

            employee.CurrentSalary = updatedEmployee.CurrentSalary;
            employee.HomeAddress = updatedEmployee.HomeAddress;

            //_context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteEmployee(Guid id)
        {
            //var employee = _context.Employees.SingleOrDefault(x => x.Id == id);
            var employee = _employees.SingleOrDefault(x => x.Id == id);

            if (employee != null)
            {
                //_context.Employees.Remove(employee);
                //_context.SaveChanges();

                _employees.Remove(employee);

                return NoContent();
            }
            
            return NotFound();
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult DeleteAllEmployees()
        {
            //_context.Employees.ExecuteDelete();
            //_context.SaveChanges();

            _employees.RemoveAll(x => true);

            return NoContent();
        }
    }
}
