using Application.Logic.Employee.Requests;
using Application.Logic.Employee.Validators;
using FluentValidation.Results;

namespace Application.Logic.Employee.ValidationHelpers
{
    public class AddEmployeeValidationHelper
    {
        public bool CanAddEmployee(AddEmployeeRequest request)
        {
            AddEmployeeValidator validator = new AddEmployeeValidator();
            var results = validator.Validate(request);

            if (!results.IsValid)
            {
                foreach (ValidationFailure failure in results.Errors)
                {
                    //TODO: Add error collection class...
                    Console.WriteLine($"{failure.PropertyName}: {failure.ErrorMessage}");
                }
            }

            return results.IsValid;
        }

    }
}
