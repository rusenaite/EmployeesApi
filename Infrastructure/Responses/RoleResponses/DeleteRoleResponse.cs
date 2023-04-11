namespace EmployeeApi.Infrastructure.Responses.Role
{
    public class DeleteRoleResponse : Response
    {
        public bool Found { get; private set; }
        public Models.RoleModels.Role? Role { get; set; }
        public DeleteRoleResponse(bool success, string? message, bool found, Models.RoleModels.Role? role) : base(success, message)
        {
            Found = found;
            Role = role;
        }

        public DeleteRoleResponse(Models.RoleModels.Role? role) : this(true, null, true, role) { }
        public DeleteRoleResponse(string message, bool found) : this(false, message, found, new Models.RoleModels.Role()) { }
    }
}
