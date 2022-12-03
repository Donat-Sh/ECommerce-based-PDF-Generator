using Core.Domain;
using FluentValidation;
using System;

namespace Core.FluentValidations
{
    public class PdfOptionsDtoValidator : AbstractValidator<PdfOptionsDto>
    {
        public PdfOptionsDtoValidator()
        {
            #region Properties

            RuleFor(x => x.ColorMode).NotNull().GreaterThan(0).WithMessage("The value for 'ColorMode' cannot be null or negative/zero");
            RuleFor(x => x.PageOrientation).NotNull().GreaterThan(0).WithMessage("The value for 'PageOrientation' cannot be null or negative/zero");
            RuleFor(x => x.PagePaperSize).NotNull().GreaterThan(0).WithMessage("The value for 'PagePaperSize' cannot be null or negative/zero");

            #endregion Properties

            #region Complex Properties

            RuleFor(x => x.PageMargins).NotNull().SetValidator(new PageMarginsDtoValidator()).WithMessage("PageMargin object cannot be null");

            #endregion Complex Properties
        }
    }
}
