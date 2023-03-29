using EmployeeApi.Api.Entities;
using EmployeeApi.Models;

namespace EmployeeApi.Infrastructure
{
    public class DataSeeder
    {
        public static void SeedData(EmployeesDbContext context)
        {
            //if (!context.Roles.Any() || !context.Employees.Any())
            //{
                //var roles = SeedRoles();
                //context.Roles.AddRange(SeedRoles());
                //context.SaveChanges();

                //context.Employees.AddRange(SeedEmployees(roles));
                //context.SaveChanges();
            //}
        }

        public static List<Role> SeedRoles()
        {
            return new List<Role>()
            {
                new Role
                {
                    Id = Guid.NewGuid(),
                    Position = Position.QA.ToString(),
                    Description = "Role for automated tester.",
                    HoursPerWeek = 40
                },
                new Role
                {
                    Id = Guid.NewGuid(),
                    Position = Position.SoftwareDeveloper.ToString(),
                    Description = "Role for .NET developer.",
                    HoursPerWeek = 40
                },
                new Role
                {
                    Id = Guid.NewGuid(),
                    Position = Position.ProductManager.ToString(),
                    Description = "Role for product manager of team of 10 people.",
                    HoursPerWeek = 30
                }
            };
        } 

        public static List<Employee> SeedEmployees(List<Role> roles)
        {
            return new List<Employee>()
            {
                new Employee {
                    Id = Guid.Parse("0D1E2AD0-4712-41CB-98A8-EB3B617F7E74"),
                    FirstName = "John",
                    LastName = "Lee",
                    BirthDate = new DateTime(1992, 10, 9).Date,
                    HomeAddress = "88 Journal Square, Jersey City",
                    CurrentSalary = 1026.30,
                    RoleId = roles[0].Id
                },
                new Employee {
                    Id = Guid.Parse("F89B1B94-58DD-492A-BDB7-F9CEFF13810F"),
                    FirstName = "Casey",
                    LastName = "Kinderly",
                    BirthDate = new DateTime(1990, 4, 16).Date,
                    HomeAddress = "65 Garnel St., Liverpool",
                    CurrentSalary = 1005.60,
                    RoleId = roles[1].Id
                },
                new Employee {
                    Id = Guid.Parse("0B9DFE0A-8628-426A-A802-DC162331F1F6"),
                    FirstName = "Harry",
                    LastName = "Lonecy",
                    BirthDate = new DateTime(1989, 9, 1).Date,
                    HomeAddress = "96 Lightbull St., London",
                    CurrentSalary = 1015.99,
                    RoleId = roles[2].Id
                }
            };
        }
    }
}
