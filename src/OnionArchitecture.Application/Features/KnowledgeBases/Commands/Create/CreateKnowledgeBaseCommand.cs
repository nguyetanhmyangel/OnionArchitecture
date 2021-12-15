using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using OnionArchitecture.Application.Interfaces.Repositories;
using OnionArchitecture.Domain.Entities;
using OnionArchitecture.Infrastructure.Share.Results;

namespace OnionArchitecture.Application.Features.KnowledgeBases.Commands.Create
{
    public partial class CreateKnowledgeBaseCommand : IRequest<Result<int>>
    {
        public int CategoryId { get; set; }

        public string Title { get; set; }

        public string SeoAlias { get; set; }

        public string Description { get; set; }

        public string Environment { get; set; }

        public string Problem { get; set; }

        public string StepToReproduce { get; set; }

        public string ErrorMessage { get; set; }

        public string Workaround { get; set; }

        public string Note { get; set; }

        public string Labels { get; set; }

        public int? NumberOfComments { get; set; }

        public int? NumberOfVotes { get; set; }

        public int? NumberOfReports { get; set; }
    }

    public class CreateKnowledgeBaseCommandHandler : IRequestHandler<CreateKnowledgeBaseCommand, Result<int>>
    {
        private readonly IKnowledgeBaseRepository _knowledgeBaseRepository;
        private readonly IMapper _mapper;

        private IUnitOfWork _unitOfWork { get; set; }

        public CreateKnowledgeBaseCommandHandler(IKnowledgeBaseRepository knowledgeBaseRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _knowledgeBaseRepository = knowledgeBaseRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<int>> Handle(CreateKnowledgeBaseCommand request, CancellationToken cancellationToken)
        {
            var knowledgeBase = _mapper.Map<KnowledgeBase>(request);
            await _knowledgeBaseRepository.InsertAsync(knowledgeBase);
            await _unitOfWork.Commit(cancellationToken);
            return await Result<int>.SuccessAsync(knowledgeBase.Id);
        }
    }
}