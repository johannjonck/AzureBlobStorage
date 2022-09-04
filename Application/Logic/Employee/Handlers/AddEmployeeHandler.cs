using Application.Dtos;
using Application.Logic.Employee.Requests;
using Application.Logic.Employee.Responses;
//using Application.Logic.Employee.ValidationHelpers;
using AutoMapper;
using MediatR;
using Persistance;

namespace Application.Logic.Employee.Handlers
{
    public class AddEmployeeHandler : HandlerBase<AddEmployeeRequest, HandlerResult<AddEmployeeResponse>>
    {
        private readonly IDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IPipelineBehavior<AddEmployeeRequest, HandlerResult<AddEmployeeResponse>> _pipelineBehavior;

        public AddEmployeeHandler(IDbContext context, IMapper mapper)//, IPipelineBehavior<AddEmployeeRequest, HandlerResult<AddEmployeeResponse>> pipelineBehavior)
        {
            _dbContext = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            //_pipelineBehavior = pipelineBehavior ?? throw new ArgumentNullException(nameof(pipelineBehavior));  
        }

        //TODO: Change to async...
        public override HandlerResult<AddEmployeeResponse> HandleRequest(AddEmployeeRequest request, CancellationToken cancellationToken, HandlerResult<AddEmployeeResponse> result)
        {
            //var canAddEmployee = new AddEmployeeValidationHelper();

            //if (canAddEmployee.CanAddEmployee(request))
            //{
            ////var blah = _pipelineBehavior.Handle;
            ////var foo = blah.BeginInvoke;

                var newEmployee = new Domain.Entities.Employee.Employee()
                {
                    FirstName = request.FirstName,
                    Surname = request.Surname,
                    IdNumber = request.IdNumber,
                    IsDeleted = request.IsDeleted,
                    EmployeeAddress = new Domain.Entities.Employee.EmployeeAddress()
                    {
                        Address1 = request.EmployeeAddressRequest.Address1,
                        Address2 = request.EmployeeAddressRequest.Address2,
                        Address3 = request.EmployeeAddressRequest.Address3,
                        Address4 = request.EmployeeAddressRequest.Address4,
                        PostalCode = request.EmployeeAddressRequest.PostalCode
                    },
                    EmployeeGroup = new Domain.Entities.Employee.EmployeeGroup()
                    {
                        Name = request.EmployeeGroupRequest.Name,
                        IsDeleted = request.EmployeeGroupRequest.IsDeleted
                    }
                };

                _dbContext.Employee.Add(newEmployee);
                _dbContext.SaveChangesAsync();

                result.Entity = new AddEmployeeResponse
                {
                    EmployeeDto = _mapper.Map<EmployeeDto>(newEmployee)
                };
            //}
            //else
            //{
            //    throw new ArgumentException();
            //}

            return result;
        }
    }
}
