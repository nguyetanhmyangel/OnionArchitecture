using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using OnionArchitecture.Application.Interfaces.Repositories;
using OnionArchitecture.Infrastructure.Share.Results;

namespace OnionArchitecture.Application.Features.Votes.Queries.GetById
{
    public class GetVoteByIdQuery : IRequest<Result<GetVoteByIdResponse>>
    {
        public int Id { get; set; }

        public class GetVoteByIdQueryHandler : IRequestHandler<GetVoteByIdQuery, Result<GetVoteByIdResponse>>
        {
            private readonly IVoteRepository _voteRepository;
            private readonly IMapper _mapper;

            public GetVoteByIdQueryHandler(IVoteRepository voteRepository, IMapper mapper)
            {
                _voteRepository = voteRepository;
                _mapper = mapper;
            }

            public async Task<Result<GetVoteByIdResponse>> Handle(GetVoteByIdQuery query, CancellationToken cancellationToken)
            {
                var vote = await _voteRepository.GetByIdAsync(query.Id);
                var mappedVote = _mapper.Map<GetVoteByIdResponse>(vote);
                return await Result<GetVoteByIdResponse>.SuccessAsync(mappedVote);
            }
        }
    }
}