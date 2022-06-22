using AutoMapper;
using NLayerCore.DTOs;
using NLayerCore.Models;

namespace NLayerService.Mapping;

public class MapProfile : Profile
{
    public MapProfile()
    {
        CreateMap<Product, ProductDto>().ReverseMap();
        CreateMap<Category, CategoryDto>().ReverseMap();
        CreateMap<ProductFeature, ProductFeatureDto>().ReverseMap();
        CreateMap<ProductUpdateDto, Product>();
        CreateMap<DetailedProductsDto, Product>().ReverseMap();
        CreateMap<CategoryProducts, Category>().ReverseMap();
    }
}