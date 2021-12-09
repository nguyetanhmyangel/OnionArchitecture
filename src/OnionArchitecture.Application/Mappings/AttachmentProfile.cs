using AutoMapper;
using OnionArchitecture.Application.Features.Attachments.Commands.Create;
using OnionArchitecture.Application.Features.Attachments.Queries.Get;
using OnionArchitecture.Application.Features.Attachments.Queries.GetById;
using OnionArchitecture.Application.Features.Attachments.Queries.GetPage;
using OnionArchitecture.Domain.Entities;

namespace OnionArchitecture.Application.Mappings
{
    internal class AttachmentProfile : Profile
    {
        public AttachmentProfile()
        {
            CreateMap<CreateAttachmentCommand, Attachment>().ReverseMap();
            CreateMap<GetAttachmentByIdResponse, Attachment>().ReverseMap();
            CreateMap<GetAttachmentResponse, Attachment>().ReverseMap();
            CreateMap<GetPageAttachmentResponse, Attachment>().ReverseMap();
        }
    }
}