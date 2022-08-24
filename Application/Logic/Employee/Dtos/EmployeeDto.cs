namespace Application.Logic.Employee.Dtos
{
    public class EmployeeDto
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string Surname { get; set; }

        public int IdNummber { get; set; }

        public bool IsDeleted { get; set; }

        public int EmployeeAddressId { get; set; }

        public int EmployeeGroupId { get; set; }

    }
}
