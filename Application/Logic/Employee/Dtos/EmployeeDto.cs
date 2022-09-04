using Application.Logic.Employee.Dtos;

namespace Application.Dtos
{
    public class EmployeeDto
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string Surname { get; set; }

        public long IdNumber { get; set; }

        public bool IsDeleted { get; set; }

        public int EmployeeAddressId { get; set; }

        public EmployeeAddressDto EmployeeAddress { get; set; }

        public EmployeeGroupDto EmployeeGroup { get; set; }

        public int EmployeeGroupId { get; set; }

    }
}
