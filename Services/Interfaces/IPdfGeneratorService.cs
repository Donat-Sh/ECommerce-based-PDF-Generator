using Core.Domain;
using System;

namespace Services.Interfaces
{
    public interface IPdfGeneratorService
    {
        #region PdfGeneration

        #region ConvertHtmlToPdf

        Task<IResult> ConvertHtmlToPdf(PdfInput pdfInput, CancellationToken cancellationToken);

        #endregion ConvertHtmlToPdf

        #region ConvertHtmlToPdf

        Task<PdfOutput> TestingConvertHtmlToPdf(PdfInput pdfInput, CancellationToken cancellationToken);

        #endregion ConvertHtmlToPdf

        #endregion PdfGeneration
    }
}
