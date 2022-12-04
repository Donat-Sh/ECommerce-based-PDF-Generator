using System;

namespace Core.Domain
{
    public class PdfInputDto
    {
        #region Properties

        public bool DownloadableProperty { get; set; }
        public string HtmlString { get; set; }
        public PdfOptionsDto Options { get; set; }

        #endregion Properties

        #region Ctor

        public PdfInputDto(string htmlString)
        {
            HtmlString = htmlString;
        }

        #endregion Ctor
    }
}
