using Core.Domain;
using Microsoft.Extensions.Logging;
using Persistence.Context;
using Services.Interfaces;
using WkHtmlToPdfDotNet;
using WkHtmlToPdfDotNet.Contracts;

namespace Services.Services
{
    public class PdfGeneratorService : IPdfGeneratorService
    {
        #region Properties

        private readonly PdfApiContext _pdfApiContext;
        private readonly ILogger<PdfGeneratorService> _logger;
        private readonly IConverter _pdfConversion;

        #endregion Properties

        #region Ctor

        public PdfGeneratorService(
                                       PdfApiContext pdfApiContext,
                                       IConverter pdfConversion,
                                       ILogger<PdfGeneratorService> logger
                                  )
        {
            _pdfApiContext = pdfApiContext;
            _logger = logger;
            _pdfConversion = pdfConversion;
        }

        #endregion Ctor

        #region Authentication

        #region ApiKey-Validation

        public async Task<bool> ValidateApiKey(string apiKeyValue, CancellationToken cancellationToken)
        {
            try
            {
                //code...

                return false;
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"Error happened on {nameof(ValidateApiKey)}, whilst attempting to Validate given ApiKey Value for Authentication!");

                throw;
            }
        }

        #endregion ApiKey-Validation

        #endregion Authentication

        #region PdfGeneration

        #region ConvertHtmlToPdf

        public async Task<PdfOutputDto> ConvertHtmlToPdf(PdfInputDto pdfInput, CancellationToken cancellationToken)
        {
            try
            {
                var convertedPdf = GetConvertedPdfDto();
                var fileNameOutput = @"ConvertedPdfDocument.pdf";

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
                            Out = fileNameOutput
                        },
                        Objects =
                        {
                            new ObjectSettings()
                            {
                                PagesCount = true,
                                HtmlContent = pdfInput.HtmlString,
                                WebSettings = { DefaultEncoding = "utf-8" },
                                //WebSettings = { DefaultEncoding = "Base64Encode" },
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
                    ByteArrayToFile(fileNameOutput, generatedPdf);
                }

                return convertedPdf;
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"Error happened on {nameof(ConvertHtmlToPdf)}, whilst attempting to Convert InputData from HTML --> PDF!");

                throw;
            }
        }

        #endregion ConvertHtmlToPdf

        #endregion PdfGeneration

        #region Helpers

        #region ByteArrayToFile

        public void ByteArrayToFile(string fileName, byte[] byteArray)
        {
            try
            {
                var fileStream = new FileStream(fileName, FileMode.Create, FileAccess.Write); //Open file for reading

                fileStream.Write(byteArray, 0, byteArray.Length); //Writes a block of bytes to this stream using data from  a byte array.

                fileStream.Close();
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"Error happened on {nameof(ByteArrayToFile)}, whilst attempting to Save .pdf file from byte[] array!");

                throw;
            }
        }

        #endregion ByteArrayToFile

        #endregion Helpers

        #region Private-Methods

        private PdfOutputDto GetConvertedPdfDto() => new PdfOutputDto("")
        {
            //code...
        };

        #endregion Private-Methods
    }
}
