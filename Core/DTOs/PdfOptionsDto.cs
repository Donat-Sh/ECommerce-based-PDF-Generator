using System;

namespace Core.Domain
{
    public class PdfOptionsDto
    {
        #region Properties

        public int ColorMode { get; set; }
        public int PageOrientation { get; set; }
        public int PagePaperSize { get; set; }
        public PageMarginsDto PageMargins { get; set; }
        public string ErrorMessage { get; set; }

        #endregion Properties

        #region Ctor

        public PdfOptionsDto(string errorMessage) => ErrorMessage = errorMessage;

        #endregion Ctor
    }
}
