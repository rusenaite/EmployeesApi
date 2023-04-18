using EmployeeApi.Infrastructure.Extensions;
using EmployeeApi.Infrastructure.Models;
using EmployeeApi.Infrastructure.Models.EmployeeModels;
using EmployeeApi.Infrastructure.Models.RoleModels;
using EmployeeApi.Infrastructure.Responses.EmployeeResponses;

namespace EmployeeApi.Infrastructure.Repositories
{
    public class EmployeeService
    {
        private readonly EmployeesDbContext _context;
        private readonly RoleService _roleService;

        public EmployeeService(EmployeesDbContext context, RoleService roleService)
        {
            _context = context;
            _roleService = roleService; 
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            return _context.Employees;
        }

        public GetEmployeesResponse GetEmployeesByRole(string role)
        {
            var foundRole = _context.Roles.FirstOrDefault(x => x.Position.Equals(role));

            if (foundRole == null)
            {
                return new GetEmployeesResponse($"Role {role} doesn't exist.");
            }

            var employees = _context.Employees
                .Where(employee => foundRole.Id == employee.RoleId)
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
            var role = _context.Roles.ToList().FirstOrDefault(x => x.Position == employeeDto.PositionName);
            
            if(role == null)
            {
                role = new Role(Guid.NewGuid(), employeeDto.PositionName);
                _context.Roles.Add(role);
            }

            var employee = new Employee()
            {
                Id = Guid.NewGuid(),
                FirstName = employeeDto.FirstName,
                LastName = employeeDto.LastName,
                BirthDate = employeeDto.BirthDate,
                HomeAddress = employeeDto.HomeAddress,
                CurrentSalary = employeeDto.CurrentSalary,
                RoleId = role.Id
            };

            var addedEmployee = _context.Employees.Add(employee);
            _context.SaveChanges();

            return new AddEmployeeResponse(addedEmployee.Entity);
        }

        public UpdateEmployeeResponse UpdateEmployee(Guid id, UpdateEmployeeDto updateEmployeeDto)
        {
            var employee = GetEmployeeFromId(id);

            if (employee == null)
            {
                return new UpdateEmployeeResponse($"Employee with id {id} does not exist.", false);
            }

            employee.CurrentSalary = updateEmployeeDto.CurrentSalary;
            employee.HomeAddress = updateEmployeeDto.HomeAddress;

            var currentRole = _roleService.GetRoleById(id);

            if (!currentRole.Success || currentRole.Role.Position == updateEmployeeDto.PositionName)
            {
                _context.SaveChanges();
                return new UpdateEmployeeResponse(employee);
            }

            var position = updateEmployeeDto.PositionName.MapStringToPosition().ToString();
            var roleFromDatabase = _roleService.GetRoleByPosition(position);

            if (!roleFromDatabase.Success)
            {
                _context.SaveChanges();
                return new UpdateEmployeeResponse(employee);
            }

            employee.RoleId = roleFromDatabase.Role.Id;

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

                return new DeleteEmployeeResponse(true, $"Employee with ID {id} was deleted.", true, employeeToDelete);
            }

            return new DeleteEmployeeResponse($"Employee with id {id} was not found.", false);
        }
    }
}
