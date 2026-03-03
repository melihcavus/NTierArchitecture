using FluentValidation;
using NTierArchitecture.Entities.DTOs;

namespace NTierArchitecture.Business.Validators
{
    public sealed class CategoryUpdateValidator : AbstractValidator<CategoryUpdateDTO>
    {
        public CategoryUpdateValidator()
        {
            RuleFor(c => c.Name).NotEmpty().WithMessage("Kategori Boş Olamaz");
        }
    }
}
