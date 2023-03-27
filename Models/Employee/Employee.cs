namespace EmployeeApi.Infrastructure
{
    public class Employee
    {
        public Guid Id { get; init; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string HomeAddress { get; set; }
        public double CurrentSalary { get; set; }
        public Guid RoleId { get; set; }

        public Employee()
        {

        }
    }
}