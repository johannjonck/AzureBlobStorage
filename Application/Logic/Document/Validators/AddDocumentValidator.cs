using Application.Logic.Document.Requests;
using FluentValidation;

namespace Application.Logic.Employee.Validators
{
    public class AddDocumentValidator : AbstractValidator<AddDocumentRequest>
    {
        public AddDocumentValidator()
        {
            RuleFor(x => x.FileName).NotNull().WithMessage("File name cnnot be empty").NotEmpty().WithMessage("File name cnnot be empty");
        }
    }
}
