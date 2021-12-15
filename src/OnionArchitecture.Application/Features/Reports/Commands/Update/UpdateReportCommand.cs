using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OnionArchitecture.Application.Interfaces.Repositories;
using OnionArchitecture.Infrastructure.Share.Results;

namespace OnionArchitecture.Application.Features.Reports.Commands.Update
{
    public class UpdateReportCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        public int KnowledgeBaseId { get; set; }
        public string Content { get; set; }
        public bool IsProcessed { get; set; }

        public class UpdateReportCommandHandler : IRequestHandler<UpdateReportCommand, Result<int>>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IReportRepository _reportRepository;

            public UpdateReportCommandHandler(IReportRepository reportRepository, IUnitOfWork unitOfWork)
            {
                _reportRepository = reportRepository;
                _unitOfWork = unitOfWork;
            }

            public async Task<Result<int>> Handle(UpdateReportCommand command, CancellationToken cancellationToken)
            {
                var report = await _reportRepository.GetByIdAsync(command.Id);

                if (report == null)
                {
                    return await Result<int>.FailAsync($"Report Not Found.");
                }

                report.Content = command.Content ?? report.Content;
                report.KnowledgeBaseId = (command.KnowledgeBaseId == 0) ? report.KnowledgeBaseId : command.KnowledgeBaseId;
                report.IsProcessed = command.IsProcessed;

                await _reportRepository.UpdateAsync(report);
                await _unitOfWork.Commit(cancellationToken);
                return await Result<int>.SuccessAsync(report.Id);
            }
        }
    }
}