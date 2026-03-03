using FluentValidation;
using NTierArchitecture.Entities.DTOs;

namespace NTierArchitecture.Business.Validators
{
    public sealed class OrderCreateValidator : AbstractValidator<OrderCreateDTO>
    {
        public OrderCreateValidator()
        {
            RuleFor(c => c.Quantity).GreaterThan(0).WithMessage("Sipariş adeti 0'dan büyük olmalıdır.");
        }
    }
}
