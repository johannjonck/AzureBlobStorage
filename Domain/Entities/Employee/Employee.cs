using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Employee
{
    public class Employee : EntityBase
    {
        public string FirstName { get; set; }

        public string Surname { get; set; }

        public int IdNummber { get; set; }

        public bool IsDeleted { get; set; }

        [Column("AddressId")]
        [ForeignKey("EmployeeAddress")]
        public int EmployeeAddressId { get; set; }

        public EmployeeAddress EmployeeAddress { get; set; }

        [Column("GroupId")]
        [ForeignKey("EmployeeGroup")]
        public int EmployeeGroupId { get; set; }

        public EmployeeGroup EmployeeGroup { get; set; }

    }
}
