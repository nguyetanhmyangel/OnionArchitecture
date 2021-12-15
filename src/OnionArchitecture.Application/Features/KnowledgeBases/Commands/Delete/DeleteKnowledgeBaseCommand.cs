using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OnionArchitecture.Application.Interfaces.Repositories;
using OnionArchitecture.Infrastructure.Share.Results;

namespace OnionArchitecture.Application.Features.KnowledgeBases.Commands.Delete
{
    public class DeleteKnowledgeBaseCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }

        public class DeleteKnowledgeBaseCommandHandler : IRequestHandler<DeleteKnowledgeBaseCommand, Result<int>>
        {
            private readonly IKnowledgeBaseRepository _knowledgeBaseRepository;
            private readonly IUnitOfWork _unitOfWork;

            public DeleteKnowledgeBaseCommandHandler(IKnowledgeBaseRepository knowledgeBaseRepository, IUnitOfWork unitOfWork)
            {
                _knowledgeBaseRepository = knowledgeBaseRepository;
                _unitOfWork = unitOfWork;
            }

            public async Task<Result<int>> Handle(DeleteKnowledgeBaseCommand command, CancellationToken cancellationToken)
            {
                var knowledgeBase = await _knowledgeBaseRepository.GetByIdAsync(command.Id);
                await _knowledgeBaseRepository.DeleteAsync(knowledgeBase);
                await _unitOfWork.Commit(cancellationToken);
                return await Result<int>.SuccessAsync(knowledgeBase.Id);
            }
        }
    }
}