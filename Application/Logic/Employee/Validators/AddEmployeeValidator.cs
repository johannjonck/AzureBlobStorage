using Application.Logic.Employee.Requests;
using FluentValidation;

namespace Application.Logic.Employee.Validators
{
    public class AddEmployeeValidator : AbstractValidator<AddEmployeeRequest>
    {
        public AddEmployeeValidator()
        {
            RuleFor(x => x.FirstName).NotNull().WithMessage(ValidationMessages.Requires_Employee_FirstName).NotEmpty().WithMessage(ValidationMessages.Requires_Employee_FirstName);
            RuleFor(x => x.Surname).NotNull().WithMessage(ValidationMessages.Requires_Employee_Surname).NotEmpty().WithMessage(ValidationMessages.Requires_Employee_Surname);
            RuleFor(x => x.IdNumber).NotNull().WithMessage(ValidationMessages.Requires_Employee_IdNumber).NotEmpty().WithMessage(ValidationMessages.Requires_Employee_IdNumber);
        }
    }
}
