using Catalog.Application.Contracts.Persistance;
using FluentValidation;

namespace Catalog.Application.Features.Products.Commands.CreateProduct
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        private readonly IProductRepository _productRepository;

        public CreateProductCommandValidator(IProductRepository productRepository)
        {
            _productRepository = productRepository;

            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

            RuleFor(p => p.CategoryId)
                .NotEmpty().WithMessage("{PropertyName} is required.");

            RuleFor(e => e)
                .MustAsync(ProductNameAndCategoryUnique)
                .WithMessage("A product with the same name under this category already exists");

            RuleFor(p => p.Price)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .GreaterThan(0);

            RuleFor(p => p.Amount)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .GreaterThan(0);

        }

        private async Task<bool> ProductNameAndCategoryUnique(CreateProductCommand e, CancellationToken token)
        {
            return !(await _productRepository.IsProductNameAndCategoryUnique(e.Name, e.CategoryId));
        }
    }
}
