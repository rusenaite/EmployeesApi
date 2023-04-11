namespace EmployeeApi.Infrastructure.Models.EmployeeModels
{
    public class EmployeeDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string HomeAddress { get; set; }
        public double CurrentSalary { get; set; }
        public Models.RoleModels.Role Role { get; set; }
        public EmployeeDto()
        {

        }
    }
}
