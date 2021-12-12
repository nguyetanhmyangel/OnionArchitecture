using AutoMapper;
using OnionArchitecture.Application.Features.Permissions.Queries.GetPage;
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
            CreateMap<CreatePermissionCommand, AppPermission>().ReverseMap();
            CreateMap<GetPermissionByIdResponse, AppPermission>().ReverseMap();
            CreateMap<GetPermissionResponse, AppPermission>().ReverseMap();
            CreateMap<GetPagePermissionResponse, AppPermission>().ReverseMap();
        }
    }
}