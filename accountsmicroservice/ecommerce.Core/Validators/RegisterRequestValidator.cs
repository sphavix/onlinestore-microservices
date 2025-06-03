using ecommerce.Core.Dtos;
using FluentValidation;

namespace ecommerce.Core.Validators;

public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
{
    public RegisterRequestValidator()
    {
        //Email
        RuleFor(temp => temp.Email)
          .NotEmpty().WithMessage("Email is required")
          .EmailAddress().WithMessage("Invalid email address format")
          ;

        //Password
        RuleFor(temp => temp.Password)
          .NotEmpty().WithMessage("Password is required")
          ;

        // Validate the PersonName property.
        RuleFor(request => request.FullName)
            .NotEmpty().WithMessage("Full Name is required")
            .Length(1, 50).WithMessage("Full Name should be 1 to 50 characters long");

        // Validate the Gender property.
        RuleFor(request => request.Gender)
            .IsInEnum().WithMessage("Invalid gender option");
    }
}
