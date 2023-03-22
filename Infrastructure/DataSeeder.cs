using EmployeeApi.Api.Entities;
using EmployeeApi.Models;

namespace EmployeeApi.Infrastructure
{
    public class DataSeeder
    {
        public static void SeedData(EmployeesDbContext context)
        {
            if (!context.Roles.Any() || !context.Employees.Any())
            {
                SeedRoles(context);
                SeedEmployees(context, context.Roles.ToList());
            }
        }

        private static void SeedRoles(EmployeesDbContext context)
        {
            var roles = new List<Role>()
            {
                new Role
                {
                    Position = Position.QA.ToString(),
                    Description = "Role for automated tester.",
                    HoursPerWeek = 40
                },
                new Role
                {
                    Position = Position.SoftwareDeveloper.ToString(),
                    Description = "Role for .NET developer.",
                    HoursPerWeek = 40
                },
                new Role
                {
                    Position = Position.ProductManager.ToString(),
                    Description = "Role for product manager of team of 10 people.",
                    HoursPerWeek = 30
                }
            };

            context.Roles.AddRange(roles);
            context.SaveChanges();
        } 

        private static void SeedEmployees(EmployeesDbContext context, List<Role> roles)
        {
            var employees = new List<Employee>()
            {
                new Employee {
                    Id = Guid.NewGuid(),
                    FirstName = "John",
                    LastName = "Lee",
                    BirthDate = new DateTime(1992, 10, 9).Date,
                    HomeAddress = "88 Journal Square, Jersey City",
                    CurrentSalary = 1026.30,
                    //Role = roles[0]
                    RoleId = roles[0].Id
                },
                new Employee {
                    Id = Guid.NewGuid(),
                    FirstName = "Casey",
                    LastName = "Kinderly",
                    BirthDate = new DateTime(1990, 4, 16).Date,
                    HomeAddress = "65 Garnel St., Liverpool",
                    CurrentSalary = 1005.60,
                    //Role = roles[1]
                    RoleId = roles[1].Id
                },
                new Employee {
                    Id = Guid.NewGuid(),
                    FirstName = "Harry",
                    LastName = "Lonecy",
                    BirthDate = new DateTime(1989, 9, 1).Date,
                    HomeAddress = "96 Lightbull St., London",
                    CurrentSalary = 1015.99,
                    //Role = roles[2]
                    RoleId = roles[2].Id
                }
            };

            context.Employees.AddRange(employees);
            context.SaveChanges();
        }
    }
}
