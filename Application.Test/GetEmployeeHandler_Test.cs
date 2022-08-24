using Application.Logic.Employee.Dtos;
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
    }
}
