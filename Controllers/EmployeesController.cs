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
        public ActionResult<IEnumerable<EmployeeDto>> GetAllEmployeesAsync()
        {
            var employees = _context.Employees.Select(employee => employee.AsDto()).ToList();

            return Ok(employees);
        }

        [HttpGet("/{name}")]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetEmployeesByNameAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return NotFound();
            }

            var employees = await _context.Employees.Where(x => x.FirstName == name).ToListAsync();

            return employees.Count is not 0 ? Ok(employees.Select(x => x.AsDto())) : NotFound();
        }

        [HttpPost]
        public void AddNewEmployeeAsync(CreateEmployeeDto employeeDto)
        {
            var employee = new Employee()
            {
                Id = Guid.NewGuid(),
                FirstName = employeeDto.FirstName,
                LastName = employeeDto.LastName,
                BirthDate = employeeDto.BirthDate,
                HomeAddress = employeeDto.HomeAddress,
                CurrentSalary = employeeDto.CurrentSalary,
                //Role = employeeDto.Role
                RoleId = employeeDto.RoleId
            };

            _context.Employees.Add(employee);
            _context.SaveChanges();
        }

        [HttpPut("/{id}")]
        public ActionResult UpdateEmployeeAsync(Guid id, UpdateEmployeeDto updatedEmployee)
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
            //employee.Role = updatedEmployee.Role;
            employee.RoleId = updatedEmployee.RoleId;

            _context.SaveChanges();

            return Ok();
        }

        [HttpDelete("/{id}")]
        public ActionResult DeleteEmployeeAsync(Guid id)
        {
            var employee = _context.Employees.SingleOrDefault(x => x.Id == id);

            if (employee != null)
            {
                _context.Employees.Remove(employee);
                _context.SaveChanges();

                return Ok();
            }
            
            return NotFound();
        }

        [HttpDelete]
        public ActionResult DeleteAllEmployeesAsync()
        {
            _context.Employees.ExecuteDelete();
            _context.SaveChanges();

            return Ok();
        }
    }
}
