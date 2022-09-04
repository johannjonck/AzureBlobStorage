using Application.Logic.Employee.Responses;
using MediatR;

namespace Application.Logic.Employee.Requests
{
    public class AddEmployeeRequest : IRequest<HandlerResult<AddEmployeeResponse>>
    {
        public string FirstName { get; set; }

        public string Surname { get; set; }

        public long IdNumber { get; set; }

        public bool IsDeleted { get; set; }

        public int EmployeeAddressId { get; set; }

        public EmployeeAddressRequest EmployeeAddressRequest { get; set; }

        public int EmployeeGroupId { get; set; }

        public EmployeeGroupRequest EmployeeGroupRequest { get; set; }

    }

    public class EmployeeAddressRequest
    {
        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string Address3 { get; set; }

        public string Address4 { get; set; }

        public int PostalCode { get; set; }

    }

    public class EmployeeGroupRequest
    {
        public string Name { get; set; }

        public bool IsDeleted { get; set; }
    }

}
