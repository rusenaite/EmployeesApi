namespace EmployeeApi.Infrastructure.Responses.EmployeeResponses
{
    public class AddEmployeeResponse : Response
    {
        public Models.EmployeeModels.Employee? Employee { get; private set; }

        public AddEmployeeResponse(bool success, string? message, Models.EmployeeModels.Employee? employee) : base(success, message)
        {
            Employee = employee;
        }

        public AddEmployeeResponse(string? message) : this(false, message, null)
        {
        }

        public AddEmployeeResponse(Models.EmployeeModels.Employee? employee) : this(true, null, employee)
        {
        }
    }
}
