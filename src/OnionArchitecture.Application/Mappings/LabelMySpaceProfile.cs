using AutoMapper;
using OnionArchitecture.Application.Features.LabelMySpaces.Commands.Create;
using OnionArchitecture.Application.Features.LabelMySpaces.Queries.Get;
using OnionArchitecture.Application.Features.LabelMySpaces.Queries.GetById;
using OnionArchitecture.Application.Features.LabelMySpaces.Queries.GetPaged;
using OnionArchitecture.Domain.Entities;

namespace OnionArchitecture.Application.Mappings
{
    internal class LabelMySpaceProfile : Profile
    {
        public LabelMySpaceProfile()
        {
            CreateMap<CreateLabelMySpaceCommand, LabelMySpace>().ReverseMap();
            CreateMap<GetLabelMySpaceByIdResponse, LabelMySpace>().ReverseMap();
            CreateMap<GetLabelMySpaceResponse, LabelMySpace>().ReverseMap();
            CreateMap<GetPageLabelMySpaceResponse, LabelMySpace>().ReverseMap();
        }
    }
}