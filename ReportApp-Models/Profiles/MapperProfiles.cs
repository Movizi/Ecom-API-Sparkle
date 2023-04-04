using AutoMapper;
using ReportApp_Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportApp_Models.Profiles
{
    public class MapperProfiles : Profile
    {
        public MapperProfiles()
        {
            // Category Map
            CreateMap<CategoryDto, Category>().ReverseMap();

            // Product Map
            CreateMap<ProductDto, Product>()
                .ReverseMap()
                .ForMember(
                    dest => dest.Category,
                    opt => opt.MapFrom(src => src.Category.CategoryName)
                )
                .ForMember(
                    dest => dest.Supplier,
                    opt => opt.MapFrom(src => src.Supplier.CompanyName)
                );

        }
    }
}
