using EmployeeApi.Infrastructure.Extensions;
using EmployeeApi.Infrastructure.Models.EmployeeModels;
using EmployeeApi.Infrastructure.Models.RoleModels;
using EmployeeApi.Infrastructure.Responses.EmployeeResponses;

namespace EmployeeApi.Infrastructure.Repositories
{
    public class EmployeeService
    {
        private readonly EmployeesDbContext _context;

        public EmployeeService(EmployeesDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            return _context.Employees;
        }

        public GetEmployeesResponse GetEmployeesByRole(string role)
        {
            var foundRole = _context.Roles.Single(x => x.Position.ToString() == role);

            if(foundRole == null)
            {
                return new GetEmployeesResponse($"Role {role} doesn't exist.");
            }

            var employees = _context.Employees
                .Where(employee => foundRole.Id == employee.RoleId && foundRole.Position.ToString() == role)
                .ToList();

            if (!employees.Any())
            {
                return new GetEmployeesResponse($"Employees with {role} doesn't exist.");
            }

            return new GetEmployeesResponse(employees.Select(x => x.AsDto(foundRole)).ToList());
        }

        public GetEmployeeResponse GetEmployeeById(Guid id)
        {
            var employee = GetEmployeeFromId(id);

            if (employee == null)
            {
                return new GetEmployeeResponse($"Employee with id {id} does not exist.");
            }

            return new GetEmployeeResponse(employee);
        }

        private Employee? GetEmployeeFromId(Guid id)
        {
            return _context.Employees.SingleOrDefault(x => x.Id == id);
        }

        public AddEmployeeResponse AddEmployee(CreateEmployeeDto employeeDto)
        {
            var employee = new Employee()
            {
                Id = Guid.NewGuid(),
                FirstName = employeeDto.FirstName,
                LastName = employeeDto.LastName,
                BirthDate = employeeDto.BirthDate,
                HomeAddress = employeeDto.HomeAddress,
                CurrentSalary = employeeDto.CurrentSalary,
                RoleId = _context.Roles.ToList().FirstOrDefault(x => x.Position.ToString() == employeeDto.PositionName)?.Id ?? new Role(Guid.NewGuid()).Id
            };

            var addedEmployee = _context.Employees.Add(employee);
            _context.SaveChanges();

            return new AddEmployeeResponse(addedEmployee.Entity);
        }

        public UpdateEmployeeResponse UpdateEmployee(Guid id, UpdateEmployeeDto updateEmployeeDto)
        {
            var employee = GetEmployeeFromId(id);

            if (employee is null)
            {
                return new UpdateEmployeeResponse($"Employee with id {id} does not exist.", false);
            }

            employee.CurrentSalary = updateEmployeeDto.CurrentSalary;
            employee.HomeAddress = updateEmployeeDto.HomeAddress;

            _context.SaveChanges();

            return new UpdateEmployeeResponse(employee);
        }

        public DeleteEmployeeResponse DeleteEmployee(Guid id)
        {
            var employeeToDelete = GetEmployeeById(id).Employee;

            if (employeeToDelete != null)
            {
                _context.Employees.Remove(employeeToDelete);
                _context.SaveChanges();

                return new DeleteEmployeeResponse(employeeToDelete);
            }

            return new DeleteEmployeeResponse($"Employee with id {id} was deleted.", false);
        }
    }
}
