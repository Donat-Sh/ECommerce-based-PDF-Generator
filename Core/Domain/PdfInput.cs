using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain
{
    public class PdfInput
    {
        public string? HtmlString { get; }
        public PdfOptions? Options { get; }

        public PdfInput(string htmlString)
        {
            HtmlString = htmlString;
        }
    }
}
