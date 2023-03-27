using EmployeeApi.Models;

namespace EmployeeApi.Infrastructure
{
    public static class RolesExtensions
    {
        public static RoleDto AsDto(this Role role)
        {
            return new RoleDto()
            {
                Position = role.Position,
                Description = role.Description,
                HoursPerWeek = role.HoursPerWeek
            };
        }
    }
}
