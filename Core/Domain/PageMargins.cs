using System;

namespace Core.Domain
{
    public class PageMargins
    {
        #region Properties

        public double? Top { get; set; }
        public double? Right { get; set; }
        public double? Bottom { get; set; }
        public double? Left { get; set; }

        #endregion Properties

        #region Ctor

        public PageMargins()
        {
            
        }

        #endregion Ctor
    }
}
