using Core.Domain;
using Services.Interfaces;
using System;

namespace Services.Services
{
    public class PdfGeneratorService : IPdfGeneratorService
    {
        #region Properties

        //code... ApiContext maybe?

        #endregion Properties

        #region Ctor

        public PdfGeneratorService()
        {
            //code...
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
