using Application.Logic.Document.Requests;
using Application.Logic.Document.Validators;
using FluentValidation;

namespace Application.Logic.Employee.Validators
{
    public class AddDocumentValidator : AbstractValidator<AddDocumentRequest>
    {
        public AddDocumentValidator()
        {
            RuleFor(x => x.FileName).NotNull().WithMessage(DocumentValidationMessages.File_Name).NotEmpty().WithMessage(DocumentValidationMessages.File_Name);
            RuleFor(x => x.FileFullName).NotNull().WithMessage(DocumentValidationMessages.File_FullName).NotEmpty().WithMessage(DocumentValidationMessages.File_FullName);
            RuleFor(x => x.FileSize).NotNull().WithMessage(DocumentValidationMessages.File_Size).GreaterThan(0).WithMessage(DocumentValidationMessages.File_Size);
        }
    }
}
