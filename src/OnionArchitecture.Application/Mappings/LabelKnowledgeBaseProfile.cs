using AutoMapper;
using OnionArchitecture.Application.Features.LabelKnowledgeBases.Commands.Create;
using OnionArchitecture.Application.Features.LabelKnowledgeBases.Queries.Get;
using OnionArchitecture.Application.Features.LabelKnowledgeBases.Queries.GetById;
using OnionArchitecture.Application.Features.LabelKnowledgeBases.Queries.GetPaged;
using OnionArchitecture.Domain.Entities;

namespace OnionArchitecture.Application.Mappings
{
    internal class LabelKnowledgeBaseProfile : Profile
    {
        public LabelKnowledgeBaseProfile()
        {
            CreateMap<CreateLabelKnowledgeBaseCommand, LabelKnowledgeBase>().ReverseMap();
            CreateMap<GetLabelKnowledgeBaseByIdResponse, LabelKnowledgeBase>().ReverseMap();
            CreateMap<GetLabelKnowledgeBaseResponse, LabelKnowledgeBase>().ReverseMap();
            CreateMap<GetPageLabelKnowledgeBaseResponse, LabelKnowledgeBase>().ReverseMap();
        }
    }
}