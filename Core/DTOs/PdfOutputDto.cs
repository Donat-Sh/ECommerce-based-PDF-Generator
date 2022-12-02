﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain
{
    public class PdfOutputDto
    {
        #region Properties

        public bool IsSuccess => string.IsNullOrEmpty(ErrorMessage);
        public string? ErrorMessage { get; private set; }
        public string? PdfDocument { get; private set; }
        public int? PdfDocumentSize { get; private set; }

        #endregion Properties

        #region Ctor

        public PdfOutputDto(string errorMessage) => ErrorMessage = errorMessage;

        public PdfOutputDto(string pdfDocument, int pdfDocumentSize)
        {
            PdfDocument = pdfDocument;
            PdfDocumentSize = pdfDocumentSize;
        }

        #endregion Ctor
    }
}