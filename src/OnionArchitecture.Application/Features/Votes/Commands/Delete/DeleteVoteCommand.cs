using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OnionArchitecture.Application.Interfaces.Repositories;
using OnionArchitecture.Infrastructure.Share.Results;

namespace OnionArchitecture.Application.Features.Votes.Commands.Delete
{
    public class DeleteVoteCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }

        public class DeleteProductCommandHandler : IRequestHandler<DeleteVoteCommand, Result<int>>
        {
            private readonly IVoteRepository _voteRepository;
            private readonly IUnitOfWork _unitOfWork;

            public DeleteProductCommandHandler(IVoteRepository voteRepository, IUnitOfWork unitOfWork)
            {
                _voteRepository = voteRepository;
                _unitOfWork = unitOfWork;
            }

            public async Task<Result<int>> Handle(DeleteVoteCommand command, CancellationToken cancellationToken)
            {
                var vote = await _voteRepository.GetByIdAsync(command.Id);
                await _voteRepository.DeleteAsync(vote);
                await _unitOfWork.Commit(cancellationToken);
                return await Result<int>.SuccessAsync(vote.Id);
            }
        }
    }
}