using AutoMapper;
using OnionArchitectureApp.Application.Dtos.ProductCategoryDtos;
using OnionArchitectureApp.Application.Dtos.ProductCategoryRelDtos;
using OnionArchitectureApp.Application.Dtos.ProductDtos;
using OnionArchitectureApp.Application.Dtos.ProductTypeDtos;
using OnionArchitectureApp.Application.Features.Commands.Products;
using OnionArchitectureApp.Application.Features.Commands.ProductTypes;
using OnionArchitectureApp.Application.Features.Queries.Products;
using OnionArchitectureApp.Domain.Entities;

namespace OnionArchitectureApp.Application.Mapping;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        //Product
        CreateMap<Product, ProductGetByIdDto>()
            .ForMember(dest => dest.TypeName, opt => opt.MapFrom(src => src.Type.Name))
            .ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src.ProductCategoryRels.Select(rel => rel.Category)))
            .ReverseMap();
        
        CreateMap<Product, ProductGetAllDto>()
            .ForMember(dest => dest.TypeName, opt => opt.MapFrom(src => src.Type.Name))
            .ReverseMap();

        CreateMap<Product, CreateProductCommand>().ReverseMap();

        //ProductCategory
        CreateMap<ProductCategory, ProductCategoryGetAllDto>().ReverseMap();

        //ProductCategoryRel
        CreateMap<ProductCategoryRel, ProductCategoryRelPostDto>().ReverseMap();

        //ProductType
        CreateMap<ProductType, ProductTypeGetAllDto>().ReverseMap();
        CreateMap<ProductType, CreateProductTypeCommand>().ReverseMap();



    }
}
