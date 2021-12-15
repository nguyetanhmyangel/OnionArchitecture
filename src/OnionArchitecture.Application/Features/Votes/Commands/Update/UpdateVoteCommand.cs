using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OnionArchitecture.Application.Interfaces.Repositories;
using OnionArchitecture.Infrastructure.Share.Results;

namespace OnionArchitecture.Application.Features.Votes.Commands.Update
{
    public class UpdateVoteCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        public int KnowledgeBaseId { get; set; }

        public class UpdateVoteCommandHandler : IRequestHandler<UpdateVoteCommand, Result<int>>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IVoteRepository _voteRepository;

            public UpdateVoteCommandHandler(IVoteRepository voteRepository, IUnitOfWork unitOfWork)
            {
                _voteRepository = voteRepository;
                _unitOfWork = unitOfWork;
            }

            public async Task<Result<int>> Handle(UpdateVoteCommand command, CancellationToken cancellationToken)
            {
                var vote = await _voteRepository.GetByIdAsync(command.Id);

                if (vote == null)
                {
                    return await Result<int>.FailAsync($"Vote Not Found.");
                }

                vote.KnowledgeBaseId = (command.KnowledgeBaseId == 0) ? vote.KnowledgeBaseId : command.KnowledgeBaseId;
                await _voteRepository.UpdateAsync(vote);
                await _unitOfWork.Commit(cancellationToken);
                return await Result<int>.SuccessAsync(vote.Id);
            }
        }
    }
}