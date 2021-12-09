using AutoMapper;
using OnionArchitecture.Application.Features.Enjoins.Commands.Create;
using OnionArchitecture.Application.Features.Enjoins.Queries.Get;
using OnionArchitecture.Application.Features.Enjoins.Queries.GetById;
using OnionArchitecture.Application.Features.Enjoins.Queries.GetPage;
using OnionArchitecture.Domain.Entities;

namespace OnionArchitecture.Application.Mappings
{
    internal class EnjoinProfile : Profile
    {
        public EnjoinProfile()
        {
            CreateMap<CreateEnjoinCommand, Enjoin>().ReverseMap();
            CreateMap<GetEnjoinByIdResponse, Enjoin>().ReverseMap();
            CreateMap<GetEnjoinResponse, Enjoin>().ReverseMap();
            CreateMap<GetPageEnjoinResponse, Enjoin>().ReverseMap();
        }
    }
}