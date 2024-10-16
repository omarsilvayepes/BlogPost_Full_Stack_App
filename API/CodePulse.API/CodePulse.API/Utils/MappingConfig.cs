﻿using AutoMapper;
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
                config.CreateMap<BlogPost, BlogPostDto>().ReverseMap();
                config.CreateMap<BlogImage, BlogImageDTO>().ReverseMap();
                //.ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src.CategoriesList));

            });
            return mapperConfig;
        }
    }
}
