using Core.Domain;
using Services.Interfaces;
using System;
using WkHtmlToPdfDotNet.Contracts;

namespace Services.Services
{
    public class PdfGeneratorService : IPdfGeneratorService
    {
        #region Properties

        private readonly IConverter _pdfConversion;

        #endregion Properties

        #region Ctor

        public PdfGeneratorService(IConverter pdfConversion)
        {
            _pdfConversion = pdfConversion;
        }

        #endregion Ctor

        #region PdfGeneration

        #region ConvertHtmlToPdf

        public async Task<PdfOutput> ConvertHtmlToPdf(PdfInput pdfInput, CancellationToken cancellationToken)
        {
            try
            {
                //code...

                return null;
            }
            catch (Exception exception)
            {
                //code...

                throw;
            } 
        }

        #endregion ConvertHtmlToPdf

        #endregion PdfGeneration
    }
}
