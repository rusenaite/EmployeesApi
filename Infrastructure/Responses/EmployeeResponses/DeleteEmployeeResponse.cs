namespace EmployeeApi.Infrastructure.Responses.EmployeeResponses
{
    public class DeleteEmployeeResponse : Response
    {
        public bool Found { get; private set; }
        public Models.EmployeeModels.Employee? Employee { get; set; }

        public DeleteEmployeeResponse(bool success, string? message, bool found, Models.EmployeeModels.Employee? employee) : base(success, message)
        {
            Found = found;
            Employee = employee;
        }

        public DeleteEmployeeResponse(Models.EmployeeModels.Employee? employee) : this(true, null, true, employee) { }
        public DeleteEmployeeResponse(string? message, bool found) : this(false, message, found, null) { }
    }
}
