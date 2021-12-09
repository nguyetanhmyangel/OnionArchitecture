using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using OnionArchitecture.Application.Interfaces.Repositories;
using OnionArchitecture.Infrastructure.Share.Results;

namespace OnionArchitecture.Application.Features.Comments.Queries.Get
{
    public class GetCommentQuery : IRequest<Result<List<GetCommentResponse>>>
    {
        public GetCommentQuery()
        {
        }
    }

    public class GetCommentQueryHandler : IRequestHandler<GetCommentQuery, Result<List<GetCommentResponse>>>
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;

        public GetCommentQueryHandler(ICommentRepository commentRepository, IMapper mapper)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
        }

        public async Task<Result<List<GetCommentResponse>>> Handle(GetCommentQuery request, CancellationToken cancellationToken)
        {
            var commentList = await _commentRepository.GetListAsync();
            var mappedComments = _mapper.Map<List<GetCommentResponse>>(commentList);
            return await Result<List<GetCommentResponse>>.SuccessAsync(mappedComments);
        }
    }
}