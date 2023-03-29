using EmployeeApi.Infrastructure;
using EmployeeApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeApi.Controllers
{
    [Route("api/roles")]
    [ApiController]
    public class RolesController : ControllerBase
    {

        //private readonly EmployeesDbContext _context;
        private List<Role> _roles;

        public RolesController()
        {
            //_context = context;
            //DataSeeder.SeedData(_context);

            _roles = DataSeeder.SeedRoles();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetRoles()
        {
            //return Ok(_context.Roles.Select(x => x.AsDto()));
            return Ok(_roles.Select(x => x.AsDto()));
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

            //_context.Roles.Add(role);
            //_context.SaveChanges();

            _roles.Add(role);

            return CreatedAtAction(nameof(AddRoles), role.Id);
        }

        [HttpPut("roles/{positionName}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateRole(string positionName, UpdateRoleDto updatedRoleDto)
        {
            //var role = _context.Roles.SingleOrDefault(x => x.Position.ToString() == positionName);
            var role = _roles.SingleOrDefault(x => x.Position.ToString() == positionName);

            if (role is null)
            {
                return NotFound("Such role does not exist.");
            }

            role.Position = updatedRoleDto.Position;
            role.Description = updatedRoleDto.Description;
            role.HoursPerWeek = updatedRoleDto.HoursPerWeek;

            //_context.SaveChanges();

            return NoContent();
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult Delete()
        {
            //_context.Roles.ExecuteDelete();
            //_context.SaveChanges();

            _roles.RemoveAll(x => true);

            return NoContent();
        }
    }
}
