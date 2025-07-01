using FluentValidation;
using OrdersService.Application.Dtos;

namespace OrdersService.Application.Validators;

public class UpdateOrderRequestValidator : AbstractValidator<UpdateOrderRequest>
{
    public UpdateOrderRequestValidator()
    {
        RuleFor(x => x.UserID)
            .NotEmpty()
            .WithMessage("User Id cannot be empty");

        RuleFor(x => x.OrderID)
            .NotEmpty()
            .WithMessage("Order Id cannot be empty");

        RuleFor(x => x.OrderDate)
            .NotEmpty()
            .WithMessage("Order Date cannot be empty");

        RuleFor(x => x.OrderItems)
            .NotEmpty()
            .WithMessage("Order must contain items");
    }
}
