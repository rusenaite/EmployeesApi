using EmployeeApi.Infrastructure.Extensions;
using EmployeeApi.Infrastructure.Models;
using EmployeeApi.Infrastructure.Models.RoleModels;
using EmployeeApi.Infrastructure.Responses.RoleResponses;

namespace EmployeeApi.Infrastructure.Repositories
{
    public class RoleService
    {
        private readonly EmployeesDbContext _context;

        public RoleService(EmployeesDbContext context)
        {
            _context = context;
        }

        public List<Role> GetRoles()
        {
            return _context.Roles.ToList();
        }

        public GetRoleReponse GetRoleByPosition(string position)
        {
            var role = _context.Roles.FirstOrDefault(x => x.Position == position);

            if (role == null)
            {
                return new GetRoleReponse($"Role by position {position} does not exist.");
            }

            return new GetRoleReponse(role.AsDto());
        }

        public GetRoleReponse GetRoleById(Guid id)
        {
            var role = _context.Roles.FirstOrDefault(x => x.Id == id);

            if (role == null)
            {
                return new GetRoleReponse($"Role with ID {id} does not exist.");
            }

            return new GetRoleReponse(role.AsDto());
        }

        public AddRoleResponse AddRole(CreateRoleDto createRoleDto)
        {
           var roleToAdd = new Role()
           {
               Id = Guid.NewGuid(),
               Position = createRoleDto.Position,
               Description = createRoleDto.Description,
               HoursPerWeek = createRoleDto.HoursPerWeek
           };

            _context.Roles.Add(roleToAdd);
            _context.SaveChanges();

            return new AddRoleResponse(roleToAdd);
        }

        public UpdateRoleResponse UpdateRole(UpdateRoleDto updatedRoleDto)
        {
            var role = _context.Roles.FirstOrDefault(x => x.Position == updatedRoleDto.Position);

            if (role == null)
            {
                return new UpdateRoleResponse($"Role of position {updatedRoleDto.Position} does not exist.", false);
            }

            if (updatedRoleDto.Position != Positions.NotDefinedYet.ToString())
            {
                role.Position = updatedRoleDto.Position;
            }

            role.Description = updatedRoleDto.Description;
            role.HoursPerWeek = updatedRoleDto.HoursPerWeek;

            _context.SaveChanges();
            return new UpdateRoleResponse(role);
        }

        public DeleteRoleResponse DeleteRole(string position)
        {
            if (position == Positions.NotDefinedYet.ToString())
            {
                return new DeleteRoleResponse("'NotDefinedYet' position cannot be deleted.", false);
            }

            var roleToDelete = _context.Roles.FirstOrDefault(x => x.Position == position);

            if (roleToDelete == null)
            {
                return new DeleteRoleResponse($"Role of position {position} does not exist.", false);
            }

            _context.Roles.Remove(roleToDelete);

            var effectedEmployees = _context.Employees.Where(x => x.RoleId == roleToDelete.Id).ToList();
            effectedEmployees.ForEach(x => x.RoleId = _context.Roles.Single(role => role.Position == Positions.NotDefinedYet.ToString()).Id);

            _context.SaveChanges();

            return new DeleteRoleResponse(true, $"Role {position} was successfully deleted.", true, roleToDelete);
        }
    }
}
