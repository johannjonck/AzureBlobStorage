using Application.Dtos;
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

            this.CreateMap<EmployeeAddress, EmployeeAddressDto>();
            this.CreateMap<EmployeeAddressDto, EmployeeAddress>();

            this.CreateMap<EmployeeGroup, EmployeeGroupDto>();
            this.CreateMap<EmployeeGroupDto, EmployeeGroup>();
        }
    }
}
