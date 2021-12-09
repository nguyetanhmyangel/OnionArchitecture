using AutoMapper;
using OnionArchitecture.Application.Features.EnjoinFunctions.Commands.Create;
using OnionArchitecture.Application.Features.EnjoinFunctions.Queries.Get;
using OnionArchitecture.Application.Features.EnjoinFunctions.Queries.GetById;
using OnionArchitecture.Application.Features.EnjoinFunctions.Queries.GetPage;
using OnionArchitecture.Domain.Entities;

namespace OnionArchitecture.Application.Mappings
{
    internal class EnjoinFunctionProfile : Profile
    {
        public EnjoinFunctionProfile()
        {
            CreateMap<CreateEnjoinFunctionCommand, EnjoinFunction>().ReverseMap();
            CreateMap<GetEnjoinFunctionByIdResponse, EnjoinFunction>().ReverseMap();
            CreateMap<GetEnjoinFunctionResponse, EnjoinFunction>().ReverseMap();
            CreateMap<GetPageEnjoinFunctionResponse, EnjoinFunction>().ReverseMap();
        }
    }
}