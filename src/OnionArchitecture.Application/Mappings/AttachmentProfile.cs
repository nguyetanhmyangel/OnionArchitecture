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
using CreateCommentCommand = OnionArchitecture.Application.Features.Categories.Commands.Create.CreateCommentCommand;

namespace OnionArchitecture.Application.Mappings
{
    internal class AttachmentProfile : Profile
    {
        public AttachmentProfile()
        {
            CreateMap<CreateCommentCommand, Attachment>().ReverseMap();
            CreateMap<GetCategoryByIdResponse, Attachment>().ReverseMap();
            CreateMap<GetAllCategoriesResponse, Attachment>().ReverseMap();
            CreateMap<GetAllCategoriesResponse, Attachment>().ReverseMap();
        }
    }
}