using AutoMapper;
using OnionArchitecture.Application.Features.Labels.Commands.Create;
using OnionArchitecture.Application.Features.Labels.Queries.Get;
using OnionArchitecture.Application.Features.Labels.Queries.GetById;
using OnionArchitecture.Application.Features.Labels.Queries.GetPage;
using OnionArchitecture.Domain.Entities;

namespace OnionArchitecture.Application.Mappings
{
    internal class LabelProfile : Profile
    {
        public LabelProfile()
        {
            CreateMap<CreateLabelCommand, Label>().ReverseMap();
            CreateMap<GetLabelByIdResponse, Label>().ReverseMap();
            CreateMap<GetLabelResponse, Label>().ReverseMap();
            CreateMap<GetPageLabelResponse, Label>().ReverseMap();
        }
    }
}