namespace Application.Dtos
{
    public class EmployeeDto
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string Surname { get; set; }

        public int IdNumber { get; set; }

        public bool IsDeleted { get; set; }

        public int EmployeeAddressId { get; set; }

        public int EmployeeGroupId { get; set; }

    }
}
