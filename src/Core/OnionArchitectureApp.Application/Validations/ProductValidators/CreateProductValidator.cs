using FluentValidation;
using OnionArchitectureApp.Application.Features.Commands.Products;

namespace OnionArchitectureApp.Application.Validations.ProductValidators;

public class CreateProductValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductValidator()
    {
        RuleFor(p => p.Name)
            .NotNull().WithMessage("Product name cannot be empty.")
            .NotEmpty().WithMessage("Product name cannot be empty.")
            .MinimumLength(3)
            .MaximumLength(100);
        RuleFor(p => p.Description)
            .MaximumLength(500);
        RuleFor(p => p.BrandName)
            .MaximumLength(500);
        RuleFor(p => p.Price)
            .NotNull().WithMessage("Product price cannot be empty.")
            .GreaterThanOrEqualTo(0);
        RuleFor(p => p.StockQuantity)
            .NotNull().WithMessage("Product stock quantity cannot be empty.")
            .GreaterThanOrEqualTo(0);
        RuleFor(p => p.TypeId)
            .NotNull().WithMessage("Product type cannot be empty.");
    }
}
