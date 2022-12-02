using System;

namespace Core.Domain
{
    public class PageMarginsDto
    {
        #region Properties

        public double? Top { get; set; }
        public double? Right { get; set; }
        public double? Bottom { get; set; }
        public double? Left { get; set; }

        #endregion Properties

        #region Ctor

        public PageMarginsDto()
        {
            
        }

        #endregion Ctor
    }
}
