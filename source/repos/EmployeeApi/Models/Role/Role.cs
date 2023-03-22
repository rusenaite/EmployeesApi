using EmployeeApi.Api.Entities;

namespace EmployeeApi.Models
{
    public class Role
    {
        public Guid Id { get; set; }
        public string Position { get; set; }
        public string Description { get; set; }
        public int HoursPerWeek { get; set; }

        public Role()
        {

        }
    }
}
