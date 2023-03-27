using EmployeeApi.Infrastructure;
using EmployeeApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {

        private readonly EmployeesDbContext _context;

        public RolesController(EmployeesDbContext context)
        {
            _context = context;
            DataSeeder.SeedData(_context);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetRoles()
        {
            return Ok(_context.Roles.Select(x => x.AsDto()));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult AddRoles(CreateRoleDto createRoleDto)
        {
            var role = new Role() 
            {
                Id = Guid.NewGuid(),
                Position = createRoleDto.Position,
                Description = createRoleDto.Description,
                HoursPerWeek = createRoleDto.HoursPerWeek
            };

            _context.Roles.Add(role);
            _context.SaveChanges();

            return CreatedAtAction(nameof(AddRoles), role.Id);
        }

        [HttpPut("/{positionName}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateRole(string positionName, UpdateRoleDto updatedRoleDto)
        {
            var role = _context.Roles.SingleOrDefault(x => x.Position.ToString() == positionName);

            if (role is null)
            {
                return NotFound("Such role does not exist.");
            }

            role.Position = updatedRoleDto.Position;
            role.Description = updatedRoleDto.Description;
            role.HoursPerWeek = updatedRoleDto.HoursPerWeek;

            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult Delete()
        {
            _context.Roles.ExecuteDelete();
            _context.SaveChanges();

            return NoContent();
        }
    }
}
