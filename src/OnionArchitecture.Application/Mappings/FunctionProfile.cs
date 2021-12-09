using AutoMapper;
using OnionArchitecture.Application.Features.Functions.Commands.Create;
using OnionArchitecture.Application.Features.Functions.Queries.Get;
using OnionArchitecture.Application.Features.Functions.Queries.GetById;
using OnionArchitecture.Application.Features.Functions.Queries.GetPage;
using OnionArchitecture.Domain.Entities;

namespace OnionArchitecture.Application.Mappings
{
    internal class FunctionProfile : Profile
    {
        public FunctionProfile()
        {
            CreateMap<CreateFunctionCommand, Function>().ReverseMap();
            CreateMap<GetFunctionByIdResponse, Function>().ReverseMap();
            CreateMap<GetFunctionResponse, Function>().ReverseMap();
            CreateMap<GetPageFunctionResponse, Function>().ReverseMap();
        }
    }
}