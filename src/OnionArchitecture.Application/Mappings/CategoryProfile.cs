using AutoMapper;
using OnionArchitecture.Application.Features.Categories.Commands.Create;
using OnionArchitecture.Application.Features.Categories.Queries.Get;
using OnionArchitecture.Application.Features.Categories.Queries.GetById;
using OnionArchitecture.Application.Features.Categories.Queries.GetPage;
using OnionArchitecture.Domain.Entities;

namespace OnionArchitecture.Application.Mappings
{
    internal class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<CreateCategoryCommand, Category>().ReverseMap();
            CreateMap<GetCategoryByIdResponse, Category>().ReverseMap();
            CreateMap<GetCategoryResponse, Category>().ReverseMap();
            CreateMap<GetPageCategoryResponse, Category>().ReverseMap();
        }
    }
}