namespace EmployeeApi.Infrastructure.Responses.RoleResponses
{
    public class AddRoleResponse : Response
    {
        public Models.RoleModels.Role? Role { get; private set; }
        public AddRoleResponse(bool success, string? message, Models.RoleModels.Role? role) : base(success, message)
        {
            Role = role;
        }

        public AddRoleResponse(Models.RoleModels.Role role) : this(true, null, role) { }
        public AddRoleResponse(string message) : this(false, message, null) { }
    }
}
