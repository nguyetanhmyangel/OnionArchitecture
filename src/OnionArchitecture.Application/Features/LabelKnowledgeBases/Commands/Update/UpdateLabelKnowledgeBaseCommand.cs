using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OnionArchitecture.Application.Interfaces.Repositories;
using OnionArchitecture.Infrastructure.Share.Results;

namespace OnionArchitecture.Application.Features.LabelKnowledgeBases.Commands.Update
{
    public class UpdateLabelKnowledgeBaseCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        public int KnowledgeBaseId { get; set; }
        public int LabelId { get; set; }

        public class UpdateLabelKnowledgeBaseCommandHandler : IRequestHandler<UpdateLabelKnowledgeBaseCommand, Result<int>>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly ILabelKnowledgeBaseRepository _labelKnowledgeBaseRepository;

            public UpdateLabelKnowledgeBaseCommandHandler(ILabelKnowledgeBaseRepository labelKnowledgeBaseRepository, IUnitOfWork unitOfWork)
            {
                _labelKnowledgeBaseRepository = labelKnowledgeBaseRepository;
                _unitOfWork = unitOfWork;
            }

            public async Task<Result<int>> Handle(UpdateLabelKnowledgeBaseCommand command, CancellationToken cancellationToken)
            {
                var labelKnowledgeBase = await _labelKnowledgeBaseRepository.GetByIdAsync(command.Id);

                if (labelKnowledgeBase == null)
                {
                    return await Result<int>.FailAsync($"Not Found.");
                }
                else
                {
                    labelKnowledgeBase.KnowledgeBaseId = (command.KnowledgeBaseId == 0) ? labelKnowledgeBase.KnowledgeBaseId : command.KnowledgeBaseId;
                    labelKnowledgeBase.LabelId = (command.LabelId == 0) ? labelKnowledgeBase.LabelId : command.LabelId;

                    await _labelKnowledgeBaseRepository.UpdateAsync(labelKnowledgeBase);
                    await _unitOfWork.Commit(cancellationToken);
                    return await Result<int>.SuccessAsync(labelKnowledgeBase.Id);
                }
            }
        }
    }
}