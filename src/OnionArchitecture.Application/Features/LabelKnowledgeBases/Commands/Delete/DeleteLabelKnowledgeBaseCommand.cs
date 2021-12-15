using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OnionArchitecture.Application.Interfaces.Repositories;
using OnionArchitecture.Infrastructure.Share.Results;

namespace OnionArchitecture.Application.Features.LabelKnowledgeBases.Commands.Delete
{
    public class DeleteLabelKnowledgeBaseCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }

        public class DeleteLabelKnowledgeBaseCommandHandler : IRequestHandler<DeleteLabelKnowledgeBaseCommand, Result<int>>
        {
            private readonly ILabelKnowledgeBaseRepository _labelKnowledgeBaseRepository;
            private readonly IUnitOfWork _unitOfWork;

            public DeleteLabelKnowledgeBaseCommandHandler(ILabelKnowledgeBaseRepository labelKnowledgeBaseRepository, IUnitOfWork unitOfWork)
            {
                _labelKnowledgeBaseRepository = labelKnowledgeBaseRepository;
                _unitOfWork = unitOfWork;
            }

            public async Task<Result<int>> Handle(DeleteLabelKnowledgeBaseCommand command, CancellationToken cancellationToken)
            {
                var labelKnowledgeBase = await _labelKnowledgeBaseRepository.GetByIdAsync(command.Id);
                await _labelKnowledgeBaseRepository.DeleteAsync(labelKnowledgeBase);
                await _unitOfWork.Commit(cancellationToken);
                return await Result<int>.SuccessAsync(labelKnowledgeBase.Id);
            }
        }
    }
}