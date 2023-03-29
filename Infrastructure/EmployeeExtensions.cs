using EmployeeApi.Models;

namespace EmployeeApi.Infrastructure
{
    public static class EmployeeExtensions
    {
        public static EmployeeDto AsDto(this Employee employee, Role role)
        {
            return new EmployeeDto()
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                CurrentSalary = employee.CurrentSalary,
                BirthDate = employee.BirthDate,
                Role = role
            };
        }
    }
}
