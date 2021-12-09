using AutoMapper;
using OnionArchitecture.Application.Features.Privileges.Commands.Create;
using OnionArchitecture.Application.Features.Privileges.Queries.Get;
using OnionArchitecture.Application.Features.Privileges.Queries.GetById;
using OnionArchitecture.Application.Features.Privileges.Queries.GetPage;
using OnionArchitecture.Domain.Entities;

namespace OnionArchitecture.Application.Mappings
{
    internal class PrivilegeProfile : Profile
    {
        public PrivilegeProfile()
        {
            CreateMap<CreatePrivilegeCommand, Privilege>().ReverseMap();
            CreateMap<GetPrivilegeByIdResponse, Privilege>().ReverseMap();
            CreateMap<GetPrivilegeResponse, Privilege>().ReverseMap();
            CreateMap<GetPagePrivilegeResponse, Privilege>().ReverseMap();
        }
    }
}