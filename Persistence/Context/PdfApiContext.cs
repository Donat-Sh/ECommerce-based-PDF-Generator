using Microsoft.EntityFrameworkCore;
using System;

namespace Persistence.Context
{
    public class PdfApiContext : DbContext
    {
        public PdfApiContext(DbContextOptions<PdfApiContext> options) : base(options)
        {
            
        }
    }
}
