using Diplomna.Models.Dtos;
using FluentValidation;

namespace Diplomna.Common.Validators
{
    public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator()
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
        }
    }
}
