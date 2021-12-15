using AutoMapper;
using OnionArchitecture.Application.Features.AppCommandFunctions.Commands.Create;
using OnionArchitecture.Application.Features.AppCommandFunctions.Queries.Get;
using OnionArchitecture.Application.Features.AppCommandFunctions.Queries.GetById;
using OnionArchitecture.Application.Features.AppCommandFunctions.Queries.GetPage;
using OnionArchitecture.Domain.Entities;

namespace OnionArchitecture.Application.Mappings
{
    internal class AppCommandFunctionProfile : Profile
    {
        public AppCommandFunctionProfile()
        {
            CreateMap<CreateAppCommandFunctionCommand, AppCommandFunction>().ReverseMap();
            CreateMap<GetAppCommandFunctionByIdResponse, AppCommandFunction>().ReverseMap();
            CreateMap<GetAppCommandFunctionResponse, AppCommandFunction>().ReverseMap();
            CreateMap<GetPageAppCommandFunctionResponse, AppCommandFunction>().ReverseMap();
        }
    }
}