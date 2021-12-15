using AutoMapper;
using OnionArchitecture.Application.Features.AppCommands.Commands.Create;
using OnionArchitecture.Application.Features.AppCommands.Queries.Get;
using OnionArchitecture.Application.Features.AppCommands.Queries.GetById;
using OnionArchitecture.Application.Features.AppCommands.Queries.GetPage;
using OnionArchitecture.Domain.Entities;

namespace OnionArchitecture.Application.Mappings
{
    internal class AppCommandProfile : Profile
    {
        public AppCommandProfile()
        {
            CreateMap<CreateAppCommand, AppCommand>().ReverseMap();
            CreateMap<GetAppCommandByIdResponse, AppCommand>().ReverseMap();
            CreateMap<GetAppCommandResponse, AppCommand>().ReverseMap();
            CreateMap<GetPageAppCommandResponse, AppCommand>().ReverseMap();
        }
    }
}