using System;

namespace Core.Domain
{
    public class PdfInput
    {
        #region Properties

        public string? HtmlString { get; }
        public PdfOptions? Options { get; }

        #endregion Properties

        #region Ctor

        public PdfInput(string htmlString)
        {
            HtmlString = htmlString;
        }

        #endregion Ctor
    }
}
