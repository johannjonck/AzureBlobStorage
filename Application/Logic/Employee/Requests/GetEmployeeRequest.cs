using Application.Logic.Employee.Responses;
using MediatR;

namespace Application.Logic.Employee.Requests
{
    public class GetEmployeeRequest : IRequest<HandlerResult<GetEmployeeResponse>>
    {
        public int EmployeeId { get; set; }

        public bool IsDeleted { get; set; }
    }
}
