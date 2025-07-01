using FluentValidation;
using OrdersService.Application.Dtos;

namespace OrdersService.Application.Validators;

public class UpdateOrderItemRequestValidator : AbstractValidator<UpdateOrderItemRequest>
{
    public UpdateOrderItemRequestValidator()
    {
        RuleFor(x => x.ProductID)
            .NotEmpty()
            .WithMessage("Product Id cannot be empty");

        RuleFor(x => x.UnitPrice)
            .NotEmpty()
            .WithMessage("Unit Price cannot be empty");

        RuleFor(x => x.Quantity)
            .NotEmpty()
            .GreaterThan(0)
            .WithMessage("Must be greater than 0");
    }
}
