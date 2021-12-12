using AutoMapper;
using OnionArchitecture.Application.Features.AppCommands.Commands.Create;
using OnionArchitecture.Application.Features.AppCommands.Queries.Get;
using OnionArchitecture.Application.Features.AppCommands.Queries.GetPage;
using OnionArchitecture.Application.Features.Enjoins.Queries.GetById;
using OnionArchitecture.Domain.Entities;

namespace OnionArchitecture.Application.Mappings
{
    internal class EnjoinProfile : Profile
    {
        public EnjoinProfile()
        {
            CreateMap<CreateAppCommand, AppCommand>().ReverseMap();
            CreateMap<GetAppCommandByIdResponse, AppCommand>().ReverseMap();
            CreateMap<GetAppCommandResponse, AppCommand>().ReverseMap();
            CreateMap<GetPageAppCommandResponse, AppCommand>().ReverseMap();
        }
    }
}