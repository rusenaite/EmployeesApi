namespace EmployeeApi.Models
{
    public class EmployeeDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string HomeAddress { get; set; }
        public double CurrentSalary { get; set; }
        public Role Role { get; set; }
        public EmployeeDto()
        {

        }
    }
}
