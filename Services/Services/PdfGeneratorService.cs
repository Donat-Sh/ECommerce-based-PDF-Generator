using Core.Domain;
using Services.Interfaces;
using System.IO;
using WkHtmlToPdfDotNet;
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

        public async Task<PdfOutputDto> ConvertHtmlToPdf(PdfInputDto pdfInput, CancellationToken cancellationToken)
        {
            try
            {
                var convertedPdf = GetConvertedPdfDto();

                if (pdfInput != null)
                {
                    var inputDoc = new HtmlToPdfDocument()
                    {
                        GlobalSettings =
                        {
                            ColorMode = (ColorMode)pdfInput.Options.ColorMode,
                            Orientation = (Orientation)pdfInput.Options.PageOrientation,
                            PaperSize = (PaperKind)pdfInput.Options.PagePaperSize,
                            Margins = new MarginSettings()
                            {
                                Top = pdfInput.Options.PageMargins.Top,
                                Right = pdfInput.Options.PageMargins.Right,
                                Bottom = pdfInput.Options.PageMargins.Bottom,
                                Left = pdfInput.Options.PageMargins.Left
                            },
                            Out = @"ConvertedPdfDocument.pdf"
                        },
                        Objects =
                        {
                            new ObjectSettings()
                            {
                                PagesCount = true,
                                HtmlContent = pdfInput.HtmlString,
                                //WebSettings = { DefaultEncoding = "utf-8" },
                                WebSettings = { DefaultEncoding = "Base64Encode" },
                                HeaderSettings =
                                {
                                    FontSize = 9,
                                    Right = "Page [page] of [toPage]",
                                    Line = true,
                                    Spacing = 2.812
                                }
                            }
                        }
                    };

                    byte[] generatedPdf = _pdfConversion.Convert(inputDoc);
                }

                return convertedPdf;
            }
            catch (Exception exception)
            {
                //code...

                throw;
            }
        }

        #endregion ConvertHtmlToPdf

        #endregion PdfGeneration

        #region Helpers

        #endregion Helpers

        #region Private-Methods

        private PdfOutputDto GetConvertedPdfDto() => new PdfOutputDto("")
        {
            //code...
        };

        #endregion Private-Methods
    }
}
