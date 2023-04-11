using EmployeeApi.Infrastructure.Models.RoleModels;

namespace EmployeeApi.Infrastructure.Responses.Role
{
    public class UpdateRoleResponse : Response
    {
        public bool Found { get; private set; }
        public Models.RoleModels.Role? Role { get; set; }
        public UpdateRoleResponse(bool success, Models.RoleModels.Role? role, string message, bool found) : base(success, message)
        {
            Found = found;
            Role = role;
        }

        public UpdateRoleResponse(Models.RoleModels.Role role) : this(true, role, null!, true) { }
        public UpdateRoleResponse(string message, bool found) : this(false, null, message, found) { }
    }
}
