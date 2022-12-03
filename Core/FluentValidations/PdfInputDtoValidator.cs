using Core.Domain;
using FluentValidation;

namespace Core.FluentValidations
{
    public class PdfInputDtoValidator : AbstractValidator<PdfInputDto>
    {
        public PdfInputDtoValidator()
        {
            #region Properties

            RuleFor(x => x.HtmlString).NotEmpty();

            #endregion Properties

            #region Complex Properties

            RuleFor(x => x.Options).SetValidator(new PdfOptionsDtoValidator());

            #endregion Complex Properties
        }
    }
}
