using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Employee
{
    public class Employee : EntityBase
    {
        public string FirstName { get; set; }

        public string Surname { get; set; }

        public int IdNumber { get; set; }

        public bool IsDeleted { get; set; }

        [ForeignKey("EmployeeAddress")]
        public int EmployeeAddressId { get; set; }

        public EmployeeAddress EmployeeAddress { get; set; }

        [ForeignKey("EmployeeGroup")]
        public int EmployeeGroupId { get; set; }

        public EmployeeGroup EmployeeGroup { get; set; }

    }
}
