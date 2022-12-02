using System;

namespace Core.Domain
{
    public class PdfOptionsDto
    {
        #region Properties

        public string? PageColorMode { get; set; }
        public string? PageOrientation { get; set; }
        public string? PagePaperSize { get; set; }
        public PageMarginsDto? PageMargins { get; set; }
        public string? ErrorMessage { get; private set; }

        #endregion Properties

        #region Ctor

        public PdfOptionsDto(string errorMessage) => ErrorMessage = errorMessage;

        #endregion Ctor
    }
}
