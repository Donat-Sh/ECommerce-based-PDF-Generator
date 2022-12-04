using Core.Domain;
using FluentValidation;

namespace Core.FluentValidations
{
    public class PdfOutputDtoValidator : AbstractValidator<PdfOutputDto>
    {
        public PdfOutputDtoValidator()
        {
            #region Properties

            RuleFor(x => x.PdfDocument).NotEmpty().WithMessage("OutPut .pdf document cannot be empty or null");
            RuleFor(x => x.PdfDocumentSize).NotEmpty().WithMessage("OutPut .pdf document size cannot be empty or null");

            #endregion Properties
        }
    }
}
