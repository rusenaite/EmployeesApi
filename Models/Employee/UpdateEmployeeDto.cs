using EmployeeApi.Api.Entities;

namespace EmployeeApi.Models
{
    public class UpdateEmployeeDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string HomeAddress { get; set; }
        public double CurrentSalary { get; set; }
        public Guid RoleId { get; set; }
        public UpdateEmployeeDto()
        {

        }
    }
}
