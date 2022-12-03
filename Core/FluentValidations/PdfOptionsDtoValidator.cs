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

            RuleFor(x => x.ColorMode).NotNull().GreaterThan(0);
            RuleFor(x => x.PageOrientation).NotNull().GreaterThan(0);
            RuleFor(x => x.PagePaperSize).NotNull().GreaterThan(0);

            #endregion Properties

            #region Complex Properties

            RuleFor(x => x.PageMargins).NotNull().SetValidator(new PageMarginsDtoValidator());

            #endregion Complex Properties
        }
    }
}
