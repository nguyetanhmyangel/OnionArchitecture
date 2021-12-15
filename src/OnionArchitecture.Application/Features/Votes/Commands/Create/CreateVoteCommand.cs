using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using OnionArchitecture.Application.Interfaces.Repositories;
using OnionArchitecture.Domain.Entities;
using OnionArchitecture.Infrastructure.Share.Results;

namespace OnionArchitecture.Application.Features.Votes.Commands.Create
{
    public partial class CreateVoteCommand : IRequest<Result<int>>
    {
        public int KnowledgeBaseId { get; set; }
    }

    public class CreateVoteCommandHandler : IRequestHandler<CreateVoteCommand, Result<int>>
    {
        private readonly IVoteRepository _voteRepository;
        private readonly IMapper _mapper;

        private IUnitOfWork _unitOfWork { get; set; }

        public CreateVoteCommandHandler(IVoteRepository voteRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _voteRepository = voteRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<int>> Handle(CreateVoteCommand request, CancellationToken cancellationToken)
        {
            var vote = _mapper.Map<Vote>(request);
            await _voteRepository.InsertAsync(vote);
            await _unitOfWork.Commit(cancellationToken);
            return await Result<int>.SuccessAsync(vote.Id);
        }
    }
}