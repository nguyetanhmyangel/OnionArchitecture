using AutoMapper;
using OnionArchitecture.Application.Features.Comments.Commands.Create;
using OnionArchitecture.Application.Features.Comments.Queries.Get;
using OnionArchitecture.Application.Features.Comments.Queries.GetById;
using OnionArchitecture.Application.Features.Comments.Queries.GetPage;
using OnionArchitecture.Domain.Entities;

namespace OnionArchitecture.Application.Mappings
{
    internal class CommentProfile : Profile
    {
        public CommentProfile()
        {
            CreateMap<CreateCommentCommand, Comment>().ReverseMap();
            CreateMap<GetCommentByIdResponse, Comment>().ReverseMap();
            CreateMap<GetCommentResponse, Comment>().ReverseMap();
            CreateMap<GetPageCommentResponse, Comment>().ReverseMap();
        }
    }
}