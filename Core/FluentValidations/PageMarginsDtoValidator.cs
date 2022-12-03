using Core.Domain;
using FluentValidation;

namespace Core.FluentValidations
{
    public class PageMarginsDtoValidator : AbstractValidator<PageMarginsDto>
    {
        public PageMarginsDtoValidator()
        {
            #region Properties

            RuleFor(x => x.Left).NotNull().WithMessage("Margin value for 'Left' cannot be null");
            RuleFor(x => x.Top).NotNull().WithMessage("Margin value for 'Top' cannot be null");
            RuleFor(x => x.Right).NotNull().WithMessage("Margin value for 'Right' cannot be null");
            RuleFor(x => x.Bottom).NotNull().WithMessage("Margin value for 'Bottom' cannot be null");
            
            #endregion Properties
        }
    }
}
