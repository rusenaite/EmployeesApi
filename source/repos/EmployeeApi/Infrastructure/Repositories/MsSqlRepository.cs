//using EmployeeApi.Api.Entities;
//using EmployeeApi.Infrastructure;
//using EmployeeApi.Models;

//namespace EmployeeApi.Api.Repositories
//{
//    public class MsSqlRepository : IEmployeesRepository
//    {
//        private readonly EmployeesDbContext _context;

//        public MsSqlRepository(EmployeesDbContext context)
//        {
//            _context = context;
//            DataSeeder.SeedData(_context);
//        }

//        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
//        {
//            return await _context.Employees;
//        }

//        public async Task<Employee?> GetAllEmployeesByNameAsync(string name)
//        {
//            var employee = await _context.Employees.SingleOrDefault(x => x.Name == name);
//            if (employee != null)
//            {
//                return employee;
//            }

//            return null;
//        }

//        public async Task<(float?, int?)> GetEmployeeCountAndAverageSalaryForRoleAsync(Position role)
//        {
//            var employees = await _context.Employees.Where(x => x.Role == role);
//            var salaryAverage = employees.Average(x => x.Salary);

//            return (salaryAverage, employees.Count());
//        }

//        public async Task AddNewEmployeeAsync(Employee employee)
//        {
//            await _context.Employees.Add(employee);
//        }

//        public async Task UpdateEmployeeAsync(Guid id, UpdateEmployeeDto updatedEmployee)
//        {
//            var employee = await _context.Employees.SingleOrDefault(x => x.Id == id);

//            employee.FirstName = updatedEmployee.FirstName;
//            employee.LastName = updatedEmployee.LastName;
//            employee.BirthDate = updatedEmployee.BirthDate;
//            employee.CurrentSalary = updatedEmployee.CurrentSalary;
//            employee.HomeAddress = updatedEmployee.HomeAddress;
//            employee.Role = updatedEmployee.Role;

//            await _context.SaveChanges();
//        }

//        public async Task UpdateEmployeeSalaryAsync(Guid id, float salary)
//        {
//            var employee = await _context.Employees.SingleOrDefault(x => x.Id == id);
//            employee.Salary = salary;

//            await _context.SaveChanges();
//        }

//        public async Task DeleteEmployeeAsync(Guid id)
//        {
//            var employee = await _context.Employees.SingleOrDefault(x => x.Id == id);
//            if (employeeToDelete != null)
//            {
//                await _context.Employees.Remove(employeeToDelete);
//            }
//        }
//    }
//}