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

            RuleFor(x => x.ColorMode).NotEmpty().WithMessage("The value for 'ColorMode' cannot be null or empty");
            RuleFor(x => x.PageOrientation).NotEmpty().WithMessage("The value for 'PageOrientation' cannot be null or empty");
            RuleFor(x => x.PagePaperSize).NotEmpty().WithMessage("The value for 'PagePaperSize' cannot be null or empty");

            #endregion Properties

            #region Complex Properties

            RuleFor(x => x.PageMargins).NotNull().SetValidator(new PageMarginsDtoValidator()).WithMessage("PageMargin object cannot be null");

            #endregion Complex Properties
        }
    }
}
