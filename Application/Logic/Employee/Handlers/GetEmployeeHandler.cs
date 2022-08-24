using Application.Logic.Employee.Requests;
using Application.Logic.Employee.Responses;
using AutoMapper;
using Persistance;
using Application.Dtos;
using Application.Logic.Employee.Dtos;

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
            var employee = _dbContext.Employee.Where(x => x.Id == request.EmployeeId && x.IsDeleted == request.IsDeleted).ToList();

            result.Entity = new GetEmployeeResponse
            {
                EmployeeDto = _mapper.Map<EmployeeDto>(employee)
            };

            return result;
        }
    }
}
