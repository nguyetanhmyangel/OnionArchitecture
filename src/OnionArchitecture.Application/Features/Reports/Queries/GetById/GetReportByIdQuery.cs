using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using OnionArchitecture.Application.Interfaces.Repositories;
using OnionArchitecture.Infrastructure.Share.Results;

namespace OnionArchitecture.Application.Features.Reports.Queries.GetById
{
    public class GetReportByIdQuery : IRequest<Result<GetReportByIdResponse>>
    {
        public int Id { get; set; }

        public class GetReportByIdQueryHandler : IRequestHandler<GetReportByIdQuery, Result<GetReportByIdResponse>>
        {
            private readonly IReportRepository _reportCache;
            private readonly IMapper _mapper;

            public GetReportByIdQueryHandler(IReportRepository reportCache, IMapper mapper)
            {
                _reportCache = reportCache;
                _mapper = mapper;
            }

            public async Task<Result<GetReportByIdResponse>> Handle(GetReportByIdQuery query, CancellationToken cancellationToken)
            {
                var report = await _reportCache.GetByIdAsync(query.Id);
                var mappedReport = _mapper.Map<GetReportByIdResponse>(report);
                return await Result<GetReportByIdResponse>.SuccessAsync(mappedReport);
            }
        }
    }
}