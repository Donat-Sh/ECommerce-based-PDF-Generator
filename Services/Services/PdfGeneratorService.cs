using AutoMapper;
using Core.Domain;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PdfApp.Attributes;
using Persistence.Context;
using Services.Interfaces;
using Services.Interfaces.Shared;
using System.Diagnostics;
using System.Net;
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
        private readonly IEnumService _enumService;
        private readonly IValidator<PdfInputDto> _validator;
        private readonly IMapper _mapper;

        #endregion Properties

        #region Ctor

        public PdfGeneratorService(
                                       PdfApiContext pdfApiContext,
                                       IConverter pdfConversion,
                                       ILogger<PdfGeneratorService> logger,
                                       IEnumService enumService,
                                       IValidator<PdfInputDto> validator,
                                       IMapper mapper
                                  )
        {
            _pdfApiContext = pdfApiContext;
            _logger = logger;
            _pdfConversion = pdfConversion;
            _enumService = enumService;
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
                var fileNameOutput = @"ConvertedPdfDocument.pdf";
                var result = await _validator.ValidateAsync(pdfInput);
                var convertedPdf = new PdfOutputDto("");

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
                            ColorMode = _enumService.GetEnumValueStringParam<ColorMode>(pdfInput.Options.ColorMode),
                            Orientation = _enumService.GetEnumValueStringParam<Orientation>(pdfInput.Options.PagePaperSize),
                            PaperSize = _enumService.GetEnumValueStringParam<PaperKind>(pdfInput.Options.PagePaperSize),
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
                                HtmlContent = !pdfInput.DownloadableProperty ? pdfInput.HtmlString : DownloadHtmlContent(pdfInput.HtmlString),
                                WebSettings = { DefaultEncoding = "utf-8" },
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

                    #region GeneratePdfByte

                    byte[] generatedPdf = _pdfConversion.Convert(inputDoc);

                    if (!(generatedPdf?.Length > 0))
                    {
                        generatedPdf = ByteInternalProcessingWKHtmlToPdf(!pdfInput.DownloadableProperty ? pdfInput.HtmlString : DownloadHtmlContent(pdfInput.HtmlString));
                    }

                    #endregion GeneratePdfByte

                    var pdfDocumentEncoding = System.Convert.ToBase64String(generatedPdf);
                    convertedPdf = GetConvertedPdfDto(pdfInput.Options.PagePaperSize, pdfDocumentEncoding);
                    _logger.LogInformation("Successful .Pdf Document Service byte[] conversion to Base64!");

                    ByteArrayToFile(fileNameOutput, generatedPdf);
                }

                return convertedPdf;
            }
            catch (Exception exception)
            {
                var errrMsg = exception.ToString();

                _logger.LogError(exception, $"Error happened on {nameof(ConvertHtmlToPdf)}, whilst attempting to Convert InputData from HTML --> PDF!");

                throw;
            }
        }

        #endregion ConvertHtmlToPdf

        #endregion PdfGeneration

        #region Helpers

        #region DownloadHtmlContent

        public string DownloadHtmlContent(string urlLink)
        {
            var htmlContent = "";

            using (var client = new WebClient())
            {
                htmlContent = client.DownloadString(urlLink);
            }

            return htmlContent;
        }

        #endregion DownloadHtmlContent

        #region ByteInternalProcessingWKHtmlToPdf

        public byte[] ByteInternalProcessingWKHtmlToPdf(string url)
        {
            var fileName = " - ";
            var wkhtmlDir = "C:\\Program Files\\wkhtmltopdf\\bin\\";
            var wkhtml = "C:\\Program Files\\wkhtmltopdf\\bin\\wkhtmltopdf.exe";
            var p = new Process();

            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.FileName = wkhtml;
            p.StartInfo.WorkingDirectory = wkhtmlDir;

            string switches = "";
            switches += "--print-media-type ";
            switches += "--margin-top 10mm --margin-bottom 10mm --margin-right 10mm --margin-left 10mm ";
            switches += "--page-size Letter ";
            p.StartInfo.Arguments = switches + " " + url + " " + fileName;
            p.Start();

            //read output
            byte[] buffer = new byte[32768];
            byte[] file;
            using (var ms = new MemoryStream())
            {
                while (true)
                {
                    int read = p.StandardOutput.BaseStream.Read(buffer, 0, buffer.Length);

                    if (read <= 0)
                    {
                        break;
                    }
                    
                    ms.Write(buffer, 0, read);
                }
                file = ms.ToArray();
            }

            p.WaitForExit(60000);
            int returnCode = p.ExitCode;
            p.Close();

            return returnCode == 0 ? file : null;
        }

        #endregion ByteInternalProcessingWKHtmlToPdf

        #region ByteArrayToFile

        public void ByteArrayToFile(string fileName, byte[] byteArray)
        {
            try
            {
                var fileStream = new FileStream(fileName, FileMode.Create, FileAccess.Write); //Open file for reading

                fileStream.Write(byteArray, 0, byteArray.Length); //Writes a block of bytes to this stream using data from  a byte array.

                fileStream.Close();

                _logger.LogInformation("Successfully Logged the .Pdf created file!");
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

        private PdfOutputDto GetConvertedPdfDto(string paperSize, string pdfDocument, string errorMessage = "") => new PdfOutputDto(errorMessage)
        {
            IsSuccess = false,
            ErrorMessage = errorMessage,
            PdfDocumentSize = paperSize,
            PdfDocument = Base64Encode(pdfDocument)
        };

        #endregion GetConvertedPdfDto

        #region GetFailedValidationPdfOutput

        private PdfOutputDto GetFailedValidationPdfOutput(string errorMessage) => new PdfOutputDto(errorMessage)
        {
            IsSuccess = true,
            ErrorMessage = errorMessage
        };

        #endregion GetFailedValidationPdfOutput

        #region Base64Encryption

        #region EncodeBase64

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            
            return System.Convert.ToBase64String(plainTextBytes);
        }

        #endregion EncodeBase64

        #region DecodeBase64

        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

        #endregion DecodeBase64

        #endregion Base64Encryption

        #endregion Private-Methods
    }
}
