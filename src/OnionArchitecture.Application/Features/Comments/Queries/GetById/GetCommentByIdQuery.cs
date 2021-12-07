using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using OnionArchitecture.Application.Interfaces.Repositories;
using OnionArchitecture.Infrastructure.Share.Results;

namespace OnionArchitecture.Application.Features.Comments.Queries.GetById
{
    public class GetCommentByIdQuery : IRequest<Result<GetCommentByIdResponse>>
    {
        public int Id { get; set; }

        public class GetCommentByIdQueryHandler : IRequestHandler<GetCommentByIdQuery, Result<GetCommentByIdResponse>>
        {
            private readonly ICommentRepository _commentRepository;
            private readonly IMapper _mapper;

            public GetCommentByIdQueryHandler(ICommentRepository commentRepository, IMapper mapper)
            {
                _commentRepository = commentRepository;
                _mapper = mapper;
            }

            public async Task<Result<GetCommentByIdResponse>> Handle(GetCommentByIdQuery query, CancellationToken cancellationToken)
            {
                var comment = await _commentRepository.GetByIdAsync(query.Id);
                var mappedComment = _mapper.Map<GetCommentByIdResponse>(comment);
                return await Result<GetCommentByIdResponse>.SuccessAsync(mappedComment);
            }
        }
    }
}