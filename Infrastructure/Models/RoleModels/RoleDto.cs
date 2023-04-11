namespace EmployeeApi.Infrastructure.Models.RoleModels
{
    public class RoleDto
    {
        public string Position { get; set; }
        public string Description { get; set; }
        public int HoursPerWeek { get; set; }

        public RoleDto()
        {

        }
    }
}
