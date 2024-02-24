using AutoMapper;
using Data.Helpers;
using Domain.Dtos.CategoryDtos;
using Domain.Dtos.ProductDtos;
using Domain.Models;

namespace Data.AutoMapper;
public class AutoMapperProfile : Profile
{

    public AutoMapperProfile()
    {
        CreateMap<CategoryInputDto, Category>();        
        
        CreateMap<Category, CategoryDetailsDto>()
               .ForMember(e => e.Picture, s => s.MapFrom(s => s.Image != null ? $"{Helper.BaseUrl}{s.Image}" : null));
        
        CreateMap<ProductInputDto, Product>();        
        
        CreateMap<Product, ProductDetailsDto>()
               .ForMember(e => e.Picture, s => s.MapFrom(s => s.Image != null ? $"{Helper.BaseUrl}{s.Image}" : null));
    }

}

