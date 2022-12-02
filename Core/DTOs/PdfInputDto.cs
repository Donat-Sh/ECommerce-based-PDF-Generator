using System;

namespace Core.Domain
{
    public class PdfInputDto
    {
        #region Properties

        public string? HtmlString { get; }
        public PdfOptionsDto? Options { get; }

        #endregion Properties

        #region Ctor

        public PdfInputDto(string htmlString)
        {
            HtmlString = htmlString;
        }

        #endregion Ctor
    }
}
