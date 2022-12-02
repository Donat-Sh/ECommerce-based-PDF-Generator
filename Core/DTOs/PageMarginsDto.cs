using System;

namespace Core.Domain
{
    public class PageMarginsDto
    {
        #region Properties

        public int Top { get; set; }
        public int Right { get; set; }
        public int Bottom { get; set; }
        public int Left { get; set; }

        #endregion Properties

        #region Ctor

        public PageMarginsDto()
        {
            
        }

        #endregion Ctor
    }
}
