using EmployeeApi.Api.Entities;
using EmployeeApi.Infrastructure;
using EmployeeApi.Models;

namespace EmployeeApi.Api.Repositories
{
    public interface IEmployeesRepository
    {
        Task<IEnumerable<Employee>> GetAllEmployeesAsync();
        Task<Employee?> GetAllEmployeesByNameAsync(string name);
        Task<(float?, int?)> GetEmployeeCountAndAverageSalaryForRoleAsync(Position role);
        Task AddNewEmployeeAsync(Employee employee);
        Task UpdateEmployeeAsync(Guid id, UpdateEmployeeDto updatedEmployee);
        Task UpdateEmployeeSalaryAsync(Guid id, float salary);
        Task DeleteEmployeeAsync(Guid id);
    }
}