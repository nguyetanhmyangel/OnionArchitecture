using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using OnionArchitecture.Application.Interfaces.Repositories;
using OnionArchitecture.Infrastructure.Share.Results;

namespace OnionArchitecture.Application.Features.Reports.Queries.Get
{
    public class GetReportQuery : IRequest<Result<List<GetReportResponse>>>
    {
        public GetReportQuery()
        {
        }
    }

    public class GetReportCachedQueryHandler : IRequestHandler<GetReportQuery, Result<List<GetReportResponse>>>
    {
        private readonly IReportRepository _reportCache;
        private readonly IMapper _mapper;

        public GetReportCachedQueryHandler(IReportRepository reportCache, IMapper mapper)
        {
            _reportCache = reportCache;
            _mapper = mapper;
        }

        public async Task<Result<List<GetReportResponse>>> Handle(GetReportQuery request, CancellationToken cancellationToken)
        {
            var reportList = await _reportCache.GetListAsync();
            var mappedReports = _mapper.Map<List<GetReportResponse>>(reportList);
            return await Result<List<GetReportResponse>>.SuccessAsync(mappedReports);
        }
    }
}