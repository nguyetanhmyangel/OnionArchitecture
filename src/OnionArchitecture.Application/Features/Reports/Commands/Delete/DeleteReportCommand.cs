using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OnionArchitecture.Application.Interfaces.Repositories;
using OnionArchitecture.Infrastructure.Share.Results;

namespace OnionArchitecture.Application.Features.Reports.Commands.Delete
{
    public class DeleteReportCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }

        public class DeleteReportCommandHandler : IRequestHandler<DeleteReportCommand, Result<int>>
        {
            private readonly IReportRepository _reportRepository;
            private readonly IUnitOfWork _unitOfWork;

            public DeleteReportCommandHandler(IReportRepository reportRepository, IUnitOfWork unitOfWork)
            {
                _reportRepository = reportRepository;
                _unitOfWork = unitOfWork;
            }

            public async Task<Result<int>> Handle(DeleteReportCommand command, CancellationToken cancellationToken)
            {
                var report = await _reportRepository.GetByIdAsync(command.Id);
                await _reportRepository.DeleteAsync(report);
                await _unitOfWork.Commit(cancellationToken);
                return await Result<int>.SuccessAsync(report.Id);
            }
        }
    }
}