using System;
using APIFinal.Models;
using APIFinal.Models.Dtos;
using AutoMapper;

namespace APIFinal.Mapping;

public class ProductProfile: Profile
{

public ProductProfile()
    {
        CreateMap<Product, ProductDto>()
        .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
        .ReverseMap();
        CreateMap<Product, CreateProductDto>().ReverseMap();
        CreateMap<Product, UpdateProductDto>().ReverseMap();
    }

}
