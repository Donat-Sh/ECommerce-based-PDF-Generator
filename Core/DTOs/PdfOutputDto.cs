using System;

namespace Core.Domain
{
    public class PdfOutputDto
    {
        #region Properties

        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }
        public string PdfDocument { get; set; }
        public string PdfDocumentSize { get; set; }

        #endregion Properties

        #region Ctor

        public PdfOutputDto(string errorMessage) => ErrorMessage = errorMessage;

        public PdfOutputDto(string pdfDocument, string pdfDocumentSize)
        {
            PdfDocument = pdfDocument;
            PdfDocumentSize = pdfDocumentSize;
        }

        #endregion Ctor
    }
}
