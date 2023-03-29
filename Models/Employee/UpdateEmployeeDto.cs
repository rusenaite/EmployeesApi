using EmployeeApi.Api.Entities;

namespace EmployeeApi.Models
{
    public class UpdateEmployeeDto
    {
        public string HomeAddress { get; set; }
        public double CurrentSalary { get; set; }
        public UpdateEmployeeDto()
        {

        }
    }
}
