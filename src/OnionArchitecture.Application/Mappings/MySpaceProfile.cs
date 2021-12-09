using AutoMapper;
using OnionArchitecture.Application.Features.MySpaces.Commands.Create;
using OnionArchitecture.Application.Features.MySpaces.Queries.Get;
using OnionArchitecture.Application.Features.MySpaces.Queries.GetById;
using OnionArchitecture.Application.Features.MySpaces.Queries.GetPage;
using OnionArchitecture.Domain.Entities;

namespace OnionArchitecture.Application.Mappings
{
    internal class MySpaceProfile : Profile
    {
        public MySpaceProfile()
        {
            CreateMap<CreateMySpaceCommand, MySpace>().ReverseMap();
            CreateMap<GetMySpaceByIdResponse, MySpace>().ReverseMap();
            CreateMap<GetMySpaceResponse, MySpace>().ReverseMap();
            CreateMap<GetPageMySpaceResponse, MySpace>().ReverseMap();
        }
    }
}