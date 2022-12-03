using Core.Domain;
using Persistence.Context;
using Services.Interfaces;
using System.IO;
using WkHtmlToPdfDotNet;
using WkHtmlToPdfDotNet.Contracts;

namespace Services.Services
{
    public class PdfGeneratorService : IPdfGeneratorService
    {
        #region Properties

        private readonly PdfApiContext _pdfApiContext;
        private readonly IConverter _pdfConversion;

        #endregion Properties

        #region Ctor

        public PdfGeneratorService(
                                       PdfApiContext pdfApiContext,
                                       IConverter pdfConversion
                                  )
        {
            _pdfApiContext = pdfApiContext;
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
                //code...

                throw;
            }
        }

        #endregion ConvertHtmlToPdf

        #endregion PdfGeneration

        #region Helpers

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
                //code...

                throw;
            }
        }

        #endregion Helpers

        #region Private-Methods

        private PdfOutputDto GetConvertedPdfDto() => new PdfOutputDto("")
        {
            //code...
        };

        #endregion Private-Methods
    }
}
