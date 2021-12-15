using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OnionArchitecture.Application.Interfaces.Repositories;
using OnionArchitecture.Infrastructure.Share.Results;

namespace OnionArchitecture.Application.Features.KnowledgeBases.Commands.Update
{
    public class UpdateKnowledgeBaseCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
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

        public class UpdateKnowledgeBaseCommandHandler : IRequestHandler<UpdateKnowledgeBaseCommand, Result<int>>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IKnowledgeBaseRepository _knowledgeBaseRepository;

            public UpdateKnowledgeBaseCommandHandler(IKnowledgeBaseRepository knowledgeBaseRepository, IUnitOfWork unitOfWork)
            {
                _knowledgeBaseRepository = knowledgeBaseRepository;
                _unitOfWork = unitOfWork;
            }

            public async Task<Result<int>> Handle(UpdateKnowledgeBaseCommand command, CancellationToken cancellationToken)
            {
                var knowledgeBase = await _knowledgeBaseRepository.GetByIdAsync(command.Id);

                if (knowledgeBase == null)
                {
                    return await Result<int>.FailAsync($"MySpace Not Found.");
                }

               
                knowledgeBase.CategoryId = (command.CategoryId == 0) ? knowledgeBase.CategoryId : command.CategoryId;
                knowledgeBase.Title = command.Title ?? knowledgeBase.Title; ;
                knowledgeBase.SeoAlias = command.SeoAlias ?? knowledgeBase.SeoAlias;
                knowledgeBase.Environment = command.Environment ?? knowledgeBase.Environment;
                knowledgeBase.Description = command.Description ?? knowledgeBase.Description;
                knowledgeBase.Problem = command.Problem ?? knowledgeBase.Problem;
                knowledgeBase.StepToReproduce = command.StepToReproduce ?? knowledgeBase.StepToReproduce;
                knowledgeBase.ErrorMessage = command.ErrorMessage ?? knowledgeBase.ErrorMessage;
                knowledgeBase.Workaround = command.Workaround ?? knowledgeBase.Workaround;
                knowledgeBase.Note = command.Note ?? knowledgeBase.Note;
                knowledgeBase.Labels = command.Labels ?? knowledgeBase.Labels;
                knowledgeBase.NumberOfComments = (command.NumberOfComments == 0) ? knowledgeBase.NumberOfComments : command.NumberOfComments;
                knowledgeBase.NumberOfVotes = (command.NumberOfVotes == 0) ? knowledgeBase.NumberOfVotes : command.NumberOfVotes;
                knowledgeBase.NumberOfReports = (command.NumberOfReports == 0) ? knowledgeBase.NumberOfReports : command.NumberOfReports;
                await _knowledgeBaseRepository.UpdateAsync(knowledgeBase);
                await _unitOfWork.Commit(cancellationToken);
                return await Result<int>.SuccessAsync(knowledgeBase.Id);
            }
        }
    }
}