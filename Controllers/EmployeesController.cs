using EmployeeApi.Infrastructure.Models.EmployeeModels;
using EmployeeApi.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeApi.Controllers
{
    [Route("api/employees")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly EmployeeService _employeeService;

        public EmployeesController(EmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<EmployeeDto>> GetAllEmployees()
        {
            return Ok(_employeeService.GetAllEmployees());
        }

        [HttpGet("/{position}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<EmployeeDto>> GetEmployeesByPosition(string position)
        {
            var response = _employeeService.GetEmployeesByRole(position);

            if (!response.Success)
            {
                if (!response.Found)
                {
                    return NotFound(response.Message);
                }
                return BadRequest(response.Message);
            }

            return Ok(response.Employees);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult AddNewEmployee(CreateEmployeeDto employeeDto)
        {
            var response = _employeeService.AddEmployee(employeeDto);

            return CreatedAtAction(nameof(AddNewEmployee), response.Employee?.Id);
        }

        [HttpPut("/{employeeId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateEmployee(Guid employeeId, UpdateEmployeeDto updatedEmployee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = _employeeService.UpdateEmployee(employeeId, updatedEmployee);

            if (!response.Success)
            {
                if (!response.Found)
                {
                    return NotFound(response.Message);
                }
                return BadRequest(response.Message);
            }

            return Ok(response.Employee);
        }

        [HttpDelete("/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteEmployee(Guid id)
        {
            var response = _employeeService.DeleteEmployee(id);
            if (!response.Success)
            {
                if (!response.Found)
                {
                    return NotFound(response.Message);
                }
                return BadRequest(response.Message);
            }

            return NoContent();
        }
    }
}
