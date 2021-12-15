using AutoMapper;
using OnionArchitecture.Application.Features.AppPermissions.Commands.Create;
using OnionArchitecture.Application.Features.AppPermissions.Queries.Get;
using OnionArchitecture.Application.Features.AppPermissions.Queries.GetById;
using OnionArchitecture.Application.Features.AppPermissions.Queries.GetPage;
using OnionArchitecture.Domain.Entities;

namespace OnionArchitecture.Application.Mappings
{
    internal class AppPermissionProfile : Profile
    {
        public AppPermissionProfile()
        {
            CreateMap<CreateAppPermissionCommand, AppPermission>().ReverseMap();
            CreateMap<GetAppPermissionByIdResponse, AppPermission>().ReverseMap();
            CreateMap<GetAppPermissionResponse, AppPermission>().ReverseMap();
            CreateMap<GetAppPagePermissionResponse, AppPermission>().ReverseMap();
        }
    }
}