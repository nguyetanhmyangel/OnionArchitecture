using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OnionArchitecture.Application.Extensions;
using OnionArchitecture.Application.Interfaces.Repositories;
using OnionArchitecture.Domain.Entities;
using OnionArchitecture.Infrastructure.Share.Results;

namespace OnionArchitecture.Application.Features.Reports.Queries.GetPage
{
    public class GetPageReportQuery : IRequest<PaginatedResult<GetPageReportResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public GetPageReportQuery(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }

    public class GetPageReportQueryHandler : IRequestHandler<GetPageReportQuery, PaginatedResult<GetPageReportResponse>>
    {
        private readonly IReportRepository _repository;

        public GetPageReportQueryHandler(IReportRepository repository)
        {
            _repository = repository;
        }

        public async Task<PaginatedResult<GetPageReportResponse>> Handle(GetPageReportQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Report, GetPageReportResponse>> expression = e => new GetPageReportResponse
            {
                Id = e.Id,
                MySpaceId = e.MySpaceId,
                Content = e.Content,
                IsProcessed = e.IsProcessed
            };
            var paginatedList = await _repository.Reports
                .Select(expression)
                .ToPaginatedListAsync(request.PageNumber, request.PageSize);
            return paginatedList;
        }
    }
}