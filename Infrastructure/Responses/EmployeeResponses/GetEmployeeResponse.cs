namespace EmployeeApi.Infrastructure.Responses.EmployeeResponses
{
    public class GetEmployeeResponse : Response
    {
        public Models.EmployeeModels.Employee? Employee { get; set; }

        public GetEmployeeResponse(bool success, string? message, Models.EmployeeModels.Employee? employee) : base(success, message)
        {
            Employee = employee;
        }

        public GetEmployeeResponse(Models.EmployeeModels.Employee? employee) : this(true, null, employee) { }
        public GetEmployeeResponse(string message) : this(false, message, null) { }
    }
}
