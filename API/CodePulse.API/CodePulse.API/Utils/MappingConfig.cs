using AutoMapper;
using CodePulse.API.Models.Domain;
using CodePulse.API.Models.DTO;

namespace CodePulse.API.Utils
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mapperConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<Category, CategoryDto>().ReverseMap(); //map also categoryDto --> Category
                config.CreateMap<BlogPost, BlogPostDto>()
                .ForMember(dest => dest.publishedDate, opt => opt.MapFrom(src => src.PublishDate));
                config.CreateMap<BlogPostDto, BlogPost>();
                config.CreateMap<BlogImage, BlogImageDTO>().ReverseMap();
            });
            return mapperConfig;
        }
    }
}
