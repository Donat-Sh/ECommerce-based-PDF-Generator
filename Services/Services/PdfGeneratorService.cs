using AutoMapper;
using Core.Domain;
using FluentValidation;
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
        private readonly IValidator<PdfInputDto> _validator;
        private readonly IMapper _mapper;

        #endregion Properties

        #region Ctor

        public PdfGeneratorService(
                                       PdfApiContext pdfApiContext,
                                       IConverter pdfConversion,
                                       ILogger<PdfGeneratorService> logger,
                                       IValidator<PdfInputDto> validator,
                                       IMapper mapper
                                  )
        {
            _pdfApiContext = pdfApiContext;
            _logger = logger;
            _pdfConversion = pdfConversion;
            _validator = validator;
            _mapper = mapper;
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
                var result = await _validator.ValidateAsync(pdfInput);

                if (!result.IsValid)
                {
                    var errorMessage = "Given PdfInputDto failed Validations";
                    _logger.LogInformation(errorMessage);
                    return GetFailedValidationPdfOutput(errorMessage);
                }

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

        #region GetConvertedPdfDto

        private PdfOutputDto GetConvertedPdfDto() => new PdfOutputDto("")
        {
            
        };

        #endregion GetConvertedPdfDto

        #region GetFailedValidationPdfOutput

        private PdfOutputDto GetFailedValidationPdfOutput(string errorMessage) => new PdfOutputDto(errorMessage)
        {
            IsSuccess = false
        };

        #endregion GetFailedValidationPdfOutput

        #endregion Private-Methods
    }
}
