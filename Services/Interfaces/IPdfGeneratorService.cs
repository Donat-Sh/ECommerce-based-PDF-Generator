using Core.Domain;
using System;

namespace Services.Interfaces
{
    public interface IPdfGeneratorService
    {
        #region Authentication

        #region ApiKey-Validation

        Task<bool> ValidateApiKey(string apiKeyValue, CancellationToken cancellationToken);

        #endregion ApiKey-Validation

        #endregion Authentication

        #region PdfGeneration

        #region ConvertHtmlToPdf

        Task<PdfOutputDto> ConvertHtmlToPdf(PdfInputDto pdfInput, CancellationToken cancellationToken);

        #endregion ConvertHtmlToPdf

        #endregion PdfGeneration
    }
}
