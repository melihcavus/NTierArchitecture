using FluentValidation;
using NTierArchitecture.Entities.DTOs;

namespace NTierArchitecture.Business.Validators
{
    public sealed class OrderUpdateValidator : AbstractValidator<OrderUpdateDTO>
    {
        public OrderUpdateValidator()
        {
            RuleFor(c => c.Quantity).GreaterThan(0).WithMessage("Sipariş adeti 0'dan büyük olmalıdır.");
        }
    }
}
