using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using OnionArchitecture.Application.Interfaces.Repositories;
using OnionArchitecture.Infrastructure.Share.Results;

namespace OnionArchitecture.Application.Features.Votes.Queries.Get
{
    public class GetVoteQuery : IRequest<Result<List<GetVoteResponse>>>
    {
        public GetVoteQuery()
        {
        }
    }

    public class GetVoteQueryHandler : IRequestHandler<GetVoteQuery, Result<List<GetVoteResponse>>>
    {
        private readonly IVoteRepository _voteRepository;
        private readonly IMapper _mapper;

        public GetVoteQueryHandler(IVoteRepository voteRepository, IMapper mapper)
        {
            _voteRepository = voteRepository;
            _mapper = mapper;
        }

        public async Task<Result<List<GetVoteResponse>>> Handle(GetVoteQuery request, CancellationToken cancellationToken)
        {
            var voteList = await _voteRepository.GetListAsync();
            var mappedVotes = _mapper.Map<List<GetVoteResponse>>(voteList);
            return await Result<List<GetVoteResponse>>.SuccessAsync(mappedVotes);
        }
    }
}