using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using OnionArchitecture.Application.Interfaces.Repositories;
using OnionArchitecture.Domain.Entities;
using OnionArchitecture.Infrastructure.Share.Results;

namespace OnionArchitecture.Application.Features.Reports.Commands.Create
{
    public partial class CreateReportCommand : IRequest<Result<int>>
    {
        public int KnowledgeBaseId { get; set; }
        public string Content { get; set; }
        public bool IsProcessed { get; set; }
    }

    public class CreateReportCommandHandler : IRequestHandler<CreateReportCommand, Result<int>>
    {
        private readonly IReportRepository _reportRepository;
        private readonly IMapper _mapper;

        private IUnitOfWork _unitOfWork { get; set; }

        public CreateReportCommandHandler(IReportRepository reportRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _reportRepository = reportRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<int>> Handle(CreateReportCommand request, CancellationToken cancellationToken)
        {
            var report = _mapper.Map<Report>(request);
            await _reportRepository.InsertAsync(report);
            await _unitOfWork.Commit(cancellationToken);
            return await Result<int>.SuccessAsync(report.Id);
        }
    }
}