using AutoMapper;
using OnionArchitecture.Application.Features.KnowledgeBases.Commands.Create;
using OnionArchitecture.Application.Features.KnowledgeBases.Queries.Get;
using OnionArchitecture.Application.Features.KnowledgeBases.Queries.GetById;
using OnionArchitecture.Application.Features.KnowledgeBases.Queries.GetPage;
using OnionArchitecture.Domain.Entities;

namespace OnionArchitecture.Application.Mappings
{
    internal class KnowledgeBaseProfile : Profile
    {
        public KnowledgeBaseProfile()
        {
            CreateMap<CreateKnowledgeBaseCommand, KnowledgeBase>().ReverseMap();
            CreateMap<GetKnowledgeBaseByIdResponse, KnowledgeBase>().ReverseMap();
            CreateMap<GetKnowledgeBaseResponse, KnowledgeBase>().ReverseMap();
            CreateMap<GetPageKnowledgeBaseResponse, KnowledgeBase>().ReverseMap();
        }
    }
}