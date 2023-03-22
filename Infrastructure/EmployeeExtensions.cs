using EmployeeApi.Models;

namespace EmployeeApi.Infrastructure
{
    public static class EmployeeExtensions
    {
        public static EmployeeDto AsDto(this Employee employee)
        {
            return new EmployeeDto()
            {
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                //Role = employee.Role,
                RoleId = employee.RoleId,
                CurrentSalary = employee.CurrentSalary,
                BirthDate = employee.BirthDate
            };
        }
    }
}
