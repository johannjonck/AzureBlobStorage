using Application.Dtos;
using Application.Logic.Employee.Requests;
using Application.Logic.Employee.Responses;
using Application.Logic.Employee.Validators;
using AutoMapper;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Persistance;

namespace Application.Logic.Employee.Handlers
{
    public class GetEmployeeHandler : HandlerBase<GetEmployeeRequest, HandlerResult<GetEmployeeResponse>>
    {
        private readonly IDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetEmployeeHandler(IDbContext context, IMapper mapper)
        {
            _dbContext = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public override HandlerResult<GetEmployeeResponse> HandleRequest(GetEmployeeRequest request, CancellationToken cancellationToken, HandlerResult<GetEmployeeResponse> result)
        {
            var employee = _dbContext.Employee
                                     .Include(ea => ea.EmployeeAddress)
                                     .Include(eg => eg.EmployeeGroup)
                                     .Where(e => e.Id == request.EmployeeId && e.IsDeleted == request.IsDeleted)
                                     .FirstOrDefault();



            result.Entity = new GetEmployeeResponse
            {
                EmployeeDto = _mapper.Map<EmployeeDto>(employee)
            };

            return result;
        }
    }
}
