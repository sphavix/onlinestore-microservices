using FluentValidation;
using OrdersService.Application.Dtos;

namespace OrdersService.Application.Validators;

public class CreateOrderRequestValidator : AbstractValidator<CreateOrderRequest>
{
    public CreateOrderRequestValidator()
    {
        RuleFor(x => x.UserID)
            .NotEmpty()
            .WithMessage("User Id cannot be empty");

        RuleFor(x => x.OrderDate)
            .NotEmpty()
            .WithMessage("User Date cannot be empty");

        RuleFor(x => x.OrderItems)
            .NotEmpty()
            .WithMessage("Order must contain items");
    }
}
