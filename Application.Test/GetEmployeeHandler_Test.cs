using Application.Logic.Employee.Handlers;
using Application.Logic.Employee.Requests;
using Application.Logic.Employee.Responses;
using Application.Tests.Handlers;
using AutoMapper;
using Domain.Entities.Employee;
using FakeItEasy;
using Infrastructure.Assemblers;
using Persistance;

namespace Application.Test
{
    [TestClass]
    public class GetEmployeeHandler_Test
    {
        private IMapper _mapper;
        private IDbContext _fakeDbContext;

        [TestInitialize]
        public void Initialize_Test()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<EmployeeMappingProfile>();
            });

            _mapper = new Mapper(config);

            _fakeDbContext = A.Fake<IDbContext>();

            MockDbContext.InitializeDatabase();
        }

        [TestMethod]
        public void GetEmployeeHandler_Constructor_Throws_ArgumentNullException_For_Null_Arguments()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new GetEmployeeHandler(null, _mapper));
            Assert.ThrowsException<ArgumentNullException>(() => new GetEmployeeHandler(_fakeDbContext, null));
        }

        [TestMethod]
        public void GetEployeeHandler_Constructor_DoesNotThrowExceptions()
        {
            try
            {
                new GetEmployeeHandler(_fakeDbContext, _mapper);
            }
            catch (Exception ex)
            {
                Assert.Fail($"Expected no exception, but got: {ex.GetType().Name}: {ex.Message}");
            }
        }

        [TestMethod]
        public async Task GetEmployeeHandler_Correctly_Finds_An_Employee_On_Request_Id()
        {
            // Setup
            var request = MockGetEmployeeRequest();
            request.EmployeeId = 1;
            request.IsDeleted = false;

            using (var mockDbContext = new MockDbContext())
            {
                var employee = new Employee
                {
                    Id = 1,
                    Surname = A.Dummy<string>(),
                    FirstName = A.Dummy<string>(),
                    IdNumber = A.Dummy<int>(),
                    EmployeeAddressId = A.Dummy<int>(),
                    EmployeeGroupId = A.Dummy<int>(),
                    IsDeleted = A.Dummy<bool>(),
                    DateModified = A.Dummy<DateTime>()
                };

                mockDbContext.Employee.Add(employee);
                await mockDbContext.SaveChangesAsync();
            }

            // Execute
            GetEmployeeResponse result_Entity;
            using (var mockDbContext = new MockDbContext())
            {
                var pc_Handler = new GetEmployeeHandler(mockDbContext, _mapper);
                var handler_Call = await pc_Handler.Handle(request, A.Dummy<CancellationToken>());

                result_Entity = handler_Call?.Entity;
            }

            // Assert
            Assert.IsNotNull(result_Entity);

            Assert.AreEqual(request.EmployeeId, result_Entity.EmployeeDto.Id);
        }

        public GetEmployeeRequest MockGetEmployeeRequest()
        {
            return new GetEmployeeRequest()
            {
                EmployeeId = A.Dummy<int>(),
                IsDeleted = A.Dummy<bool>()
            };
        }
    }
}
