using Application.Logic.Employee.Dtos;
using AutoMapper;
using Domain.Entities.Employee;

namespace Infrastructure.Assemblers
{
    public class EmployeeMappingProfile : Profile
    {
        public EmployeeMappingProfile()
        {
            this.CreateMap<Employee, EmployeeDto>();
            this.CreateMap<EmployeeDto, Employee>();
        }
    }
}
