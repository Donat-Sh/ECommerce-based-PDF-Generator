using AutoMapper;
using Core.Domain;

namespace Configurations.AutoMappings
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            #region PdfInput

            CreateMap<PdfInputDto, PdfInput>()
                .ReverseMap();

            #endregion PdfInput

            #region PdfOptions

            CreateMap<PdfOptionsDto, PdfOptions>()
                .ReverseMap();

            #endregion PdfOptions

            #region PdfOptions

            CreateMap<PdfOptionsDto, PdfOptions>()
                .ReverseMap();

            #endregion PdfOptions

            #region PdfOutput

            CreateMap<PdfOutputDto, PdfOutput>()
                .ReverseMap();

            #endregion PdfOutput
        }
    }
}
