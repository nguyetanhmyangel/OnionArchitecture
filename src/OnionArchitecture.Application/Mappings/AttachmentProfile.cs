//using AspNetCoreHero.Boilerplate.Application.Features.Attachments.Commands.Create;
//using AspNetCoreHero.Boilerplate.Application.Features.Attachments.Queries.GetAllCached;
//using AspNetCoreHero.Boilerplate.Application.Features.Attachments.Queries.GetAllPaged;
//using AspNetCoreHero.Boilerplate.Application.Features.Attachments.Queries.GetById;
//using AspNetCoreHero.Boilerplate.Domain.Entities.Catalog;

using AutoMapper;
using OnionArchitecture.Application.Features.Attachments.Commands.Create;
using OnionArchitecture.Application.Features.Attachments.Queries.GetAll;
using OnionArchitecture.Application.Features.Attachments.Queries.GetById;
using OnionArchitecture.Domain.Entities;

namespace OnionArchitecture.Application.Mappings
{
    internal class AttachmentProfile : Profile
    {
        public AttachmentProfile()
        {
            CreateMap<CreateCategoryCommand, Attachment>().ReverseMap();
            CreateMap<GetAttachmentByIdResponse, Attachment>().ReverseMap();
            CreateMap<GetAllCategoriesResponse, Attachment>().ReverseMap();
            CreateMap<GetAllCategoriesResponse, Attachment>().ReverseMap();
        }
    }
}