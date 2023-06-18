using Diplomna.Common.Dtos;
using FluentValidation;

namespace Diplomna.Common.Validators
{
    public class RegisterMobileRequestValidator : AbstractValidator<RegisterMobileRequest>
    {
        public RegisterMobileRequestValidator()
        {

            RuleFor(p => p.FacultyNumber)
                .Must((fn) =>
                {
                    var isNumber = int.TryParse(fn, out int result);

                    return isNumber && result >= 100;
                })
                .WithMessage("Invalid faculty number.");

            RuleFor(p => p.Token)
                .NotEmpty()
                .WithMessage("Invalid request.");

            RuleFor(p => p.FirstName)
                .NotEmpty()
                .WithMessage("Invalid first name");

            RuleFor(p => p.LastName)
                .NotEmpty()
                .WithMessage("Invalid last name");
        }
    }
}
