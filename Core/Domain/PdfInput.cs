using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
