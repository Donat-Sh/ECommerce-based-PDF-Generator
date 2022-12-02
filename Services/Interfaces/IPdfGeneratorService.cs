using Core.Domain;
using System;

namespace Services.Interfaces
{
    public interface IPdfGeneratorService
    {
        #region PdfGeneration

        #region ConvertHtmlToPdf

        Task<PdfOutput> ConvertHtmlToPdf(PdfInput pdfInput, CancellationToken cancellationToken);

        #endregion ConvertHtmlToPdf

        #endregion PdfGeneration
    }
}
