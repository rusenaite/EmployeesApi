using EmployeeApi.Infrastructure.Extensions;
using EmployeeApi.Infrastructure.Models.RoleModels;
using EmployeeApi.Infrastructure.Responses.Role;
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

            if(role == null)
            {
                return new GetRoleReponse($"Role by position {position} does not exist.");
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

        public UpdateRoleResponse UpdateRole(string positionName, UpdateRoleDto updatedRoleDto)
        {
            var role = _context.Roles.SingleOrDefault(x => x.Position.ToString() == positionName);

            if (role is null)
            {
                return new UpdateRoleResponse($"Role of position {positionName} does not exist.", false);
            }

            role.Position = updatedRoleDto.Position;
            role.Description = updatedRoleDto.Description;
            role.HoursPerWeek = updatedRoleDto.HoursPerWeek;

            _context.SaveChanges();

            return new UpdateRoleResponse(role);
        }

        public DeleteRoleResponse DeleteRole(string position)
        {
            var role = _context.Roles.SingleOrDefault(x => x.Position.ToString() == position);

            if (role is null)
            {
                return new DeleteRoleResponse($"Role of position {position} does not exist.", false);
            }

            _context.Roles.Remove(role);
            _context.SaveChanges();

            return new DeleteRoleResponse(role);
        }
    }
}
