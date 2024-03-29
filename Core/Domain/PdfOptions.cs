﻿using System;

namespace Core.Domain
{
    public class PdfOptions
    {
        #region Properties

        public string? PageColorMode { get; set; }
        public string? PageOrientation { get; set; }
        public string? PagePaperSize { get; set; }
        public PageMargins? PageMargins { get; set; }
        public string? ErrorMessage { get; private set; }

        #endregion Properties

        #region Ctor

        public PdfOptions(string errorMessage) => ErrorMessage = errorMessage;

        #endregion Ctor
    }
}
