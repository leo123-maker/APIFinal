using System;
using APIFinal.Models.Dtos;
using AutoMapper;

namespace  APIFinal.Mapping;

    public class CategoryProfile: Profile
    {
        
        public CategoryProfile()
        {
            CreateMap<Category,CategoryDto>().ReverseMap();
            CreateMap<Category,CreatecategoruSto>().ReverseMap();
        }
    }
