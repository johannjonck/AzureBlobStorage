using Application.Logic.Employee.Requests;
using FluentValidation;

namespace Application.Logic.Employee.Validators
{
    public class AddEmployeeValidator : AbstractValidator<AddEmployeeRequest>
    {
        public AddEmployeeValidator()
        {
            RuleFor(x => x.FirstName).NotNull().WithMessage(EmployeeValidationMessages.Requires_Employee_FirstName).NotEmpty().WithMessage(EmployeeValidationMessages.Requires_Employee_FirstName);
            RuleFor(x => x.Surname).NotNull().WithMessage(EmployeeValidationMessages.Requires_Employee_Surname).NotEmpty().WithMessage(EmployeeValidationMessages.Requires_Employee_Surname);
            RuleFor(x => x.IdNumber).NotNull().WithMessage(EmployeeValidationMessages.Requires_Employee_IdNumber).NotEmpty().WithMessage(EmployeeValidationMessages.Requires_Employee_IdNumber);
        }
    }
}
