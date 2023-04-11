namespace EmployeeApi.Infrastructure.Models.RoleModels
{
    public class Role
    {
        public Guid Id { get; set; }
        public string Position { get; set; }
        public string Description { get; set; }
        public int HoursPerWeek { get; set; }

        public Role() { }

        public Role(Guid id)
        {
            Id = id;
            Position = Positions.NotDefinedYet.ToString();
            Description = "Role not defined yet.";
            HoursPerWeek = 40;
        }
    }
}
