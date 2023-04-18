namespace EmployeeApi.Infrastructure.Models.RoleModels
{
    public class Role
    {
        public Guid Id { get; set; }
        public string Position { get; set; }
        public string Description { get; set; }
        public int HoursPerWeek { get; set; }

        public Role() { }

        public Role(Guid id, string position)
        {
            Id = id;
            Position = position.MapStringToPosition().ToString();
            Description = "Role not defined yet.";
            HoursPerWeek = 40;
        }
    }
}
