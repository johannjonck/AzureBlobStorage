namespace Application.Logic.Employee.Validators
{
    public class EmployeeValidationMessages
    {
        private static readonly string Request_Name = "AddEmployeeRequest:";

        public static string Requires_Employee_FirstName = $"{Request_Name} Employee first name must be specified.";
        public static string Requires_Employee_Surname = $"{Request_Name} Employee surname must be specified.";
        public static string Requires_Employee_IdNumber = $"{Request_Name} Employee id number must be specified.";
    }
}
