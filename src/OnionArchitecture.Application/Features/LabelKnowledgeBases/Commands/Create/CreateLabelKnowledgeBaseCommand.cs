using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using OnionArchitecture.Application.Interfaces.Repositories;
using OnionArchitecture.Domain.Entities;
using OnionArchitecture.Infrastructure.Share.Results;

namespace OnionArchitecture.Application.Features.LabelKnowledgeBases.Commands.Create
{
    public partial class CreateLabelKnowledgeBaseCommand : IRequest<Result<int>>
    {
        public int KnowledgeBaseId { get; set; }
        public int LabelId { get; set; }
    }

    public class CreateLabelKnowledgeBaseCommandHandler : IRequestHandler<CreateLabelKnowledgeBaseCommand, Result<int>>
    {
        private readonly ILabelKnowledgeBaseRepository _labelKnowledgeBaseRepository;
        private readonly IMapper _mapper;

        private IUnitOfWork _unitOfWork { get; set; }

        public CreateLabelKnowledgeBaseCommandHandler(ILabelKnowledgeBaseRepository labelKnowledgeBaseRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _labelKnowledgeBaseRepository = labelKnowledgeBaseRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<int>> Handle(CreateLabelKnowledgeBaseCommand request, CancellationToken cancellationToken)
        {
            var labelKnowledgeBase = _mapper.Map<LabelKnowledgeBase>(request);
            await _labelKnowledgeBaseRepository.InsertAsync(labelKnowledgeBase);
            await _unitOfWork.Commit(cancellationToken);
            return await Result<int>.SuccessAsync(labelKnowledgeBase.Id);
        }
    }
}