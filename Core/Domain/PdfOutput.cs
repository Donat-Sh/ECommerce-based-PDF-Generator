using System;

namespace Core.Domain
{
    public class PdfOutput
    {
        #region Properties

        public bool IsSuccess => string.IsNullOrEmpty(ErrorMessage);
        public string? ErrorMessage { get; private set; }
        public string? PdfDocument { get; private set; }
        public int? PdfDocumentSize { get; private set; }

        #endregion Properties

        #region Ctor

        public PdfOutput(string errorMessage) => ErrorMessage = errorMessage;

        public PdfOutput(string pdfDocument, int pdfDocumentSize)
        {
            PdfDocument = pdfDocument;
            PdfDocumentSize = pdfDocumentSize;
        }

        #endregion Ctor
    }
}
