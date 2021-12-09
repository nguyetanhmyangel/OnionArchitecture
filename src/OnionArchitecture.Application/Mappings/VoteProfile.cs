using AutoMapper;
using OnionArchitecture.Application.Features.Votes.Commands.Create;
using OnionArchitecture.Application.Features.Votes.Queries.Get;
using OnionArchitecture.Application.Features.Votes.Queries.GetById;
using OnionArchitecture.Application.Features.Votes.Queries.GetPage;
using OnionArchitecture.Domain.Entities;

namespace OnionArchitecture.Application.Mappings
{
    internal class VoteProfile : Profile
    {
        public VoteProfile()
        {
            CreateMap<CreateVoteCommand, Vote>().ReverseMap();
            CreateMap<GetVoteByIdResponse, Vote>().ReverseMap();
            CreateMap<GetVoteResponse, Vote>().ReverseMap();
            CreateMap<GetPageVoteResponse, Vote>().ReverseMap();
        }
    }
}