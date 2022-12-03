using Core.Domain;
using FluentValidation;

namespace Core.FluentValidations
{
    public class PdfInputDtoValidator : AbstractValidator<PdfInputDto>
    {
        public PdfInputDtoValidator()
        {
            #region Properties

            RuleFor(x => x.HtmlString).NotEmpty().WithMessage("The HTML string to convert to a .pdf document cannot ever be empty or null");

            #endregion Properties

            #region Complex Properties

            RuleFor(x => x.Options).NotNull().SetValidator(new PdfOptionsDtoValidator()).WithMessage("Options object cannot be null");

            #endregion Complex Properties
        }
    }
}
