using Application.Logic.Employee.Handlers;
using Application.Logic.Employee.Requests;
using Application.Logic.Employee.Responses;
using Application.Tests.Handlers;
using AutoMapper;
using FakeItEasy;
using Infrastructure.Assemblers;
using MediatR;
using Persistance;

namespace Application.Test.Employee
{
    [TestClass]
    public class AddEmployeeHandler_Test
    {
        private IMapper _mapper;
        private IDbContext _fakeDbContext;
        //private IPipelineBehavior<AddEmployeeRequest, HandlerResult<AddEmployeeResponse>> _pipelineBehavior;

        [TestInitialize]
        public void Initialize_Test()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<EmployeeMappingProfile>();
            });

            _mapper = new Mapper(config);

           // _pipelineBehavior = A.Fake<IPipelineBehavior<AddEmployeeRequest, HandlerResult<AddEmployeeResponse>>>(); // IPipelineBehavior<AddEmployeeRequest, HandlerResult<AddEmployeeResponse>>;

            _fakeDbContext = A.Fake<IDbContext>();

            MockDbContext.InitializeDatabase();
        }

        [TestMethod]
        public async Task AddEmployeeHandler_Correctly_Insert_An_Employee_On_Request()
        {
            var request = MockAddEmployeeRequest();
            AddEmployeeResponse result_Entity;

            using (var mockDbContext = new MockDbContext())
            {
                var pc_Handler = new AddEmployeeHandler(mockDbContext, _mapper); //, _pipelineBehavior);
                var handler_Call = await pc_Handler.Handle(request, A.Dummy<CancellationToken>());

                result_Entity = handler_Call?.Entity;
            }

            Assert.IsNotNull(result_Entity);
        }

        public AddEmployeeRequest MockAddEmployeeRequest()
        {
            return new AddEmployeeRequest()
            {
                FirstName = "", //A.Dummy<string>(), //"Piet",
                Surname   = "Skiet",
                IdNumber = 5011154051077,
                IsDeleted = A.Dummy<bool>(),
                EmployeeAddressRequest = new EmployeeAddressRequest()
                {
                    Address1 = A.Dummy<string>(),
                    Address2 = A.Dummy<string>(),
                    Address3 = A.Dummy<string>(),
                    Address4 = A.Dummy<string>(),
                    PostalCode =   A.Dummy<int>()
                },
                EmployeeGroupRequest = new EmployeeGroupRequest()
                {
                    Name =  A.Dummy<string>(),
                    IsDeleted = A.Dummy<bool>()
                }
            };
        }

    }
}
