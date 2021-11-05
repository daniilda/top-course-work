using AutoMapper;
using Microsoft.AspNetCore.Http;
using TopCourseWorkBl.BusinessLayer.Handlers.Common.UploadDataset;

namespace TopCourseWorkBl.MapperProfiles
{
    public class CommonProfile : Profile
    {
        public CommonProfile()
        {
            CreateMap<IFormFileCollection, UploadDatasetCommand>()
                .ForMember(x => x.Files, opt => opt.MapFrom(y => y));
        }
    }
}