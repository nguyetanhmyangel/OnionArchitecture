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

namespace OnionArchitecture.Application.Features.Enjoins.Queries.GetPage
{
    public class GetPageEnjoinQuery : IRequest<PaginatedResult<GetPageEnjoinResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public GetPageEnjoinQuery(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }

    public class GetPageEnjoinQueryHandler : IRequestHandler<GetPageEnjoinQuery, PaginatedResult<GetPageEnjoinResponse>>
    {
        private readonly IEnjoinRepository _repository;

        public GetPageEnjoinQueryHandler(IEnjoinRepository repository)
        {
            _repository = repository;
        }

        public async Task<PaginatedResult<GetPageEnjoinResponse>> Handle(GetPageEnjoinQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Enjoin, GetPageEnjoinResponse>> expression = e => new GetPageEnjoinResponse
            {
                Id = e.Id,
                Name = e.Name
            };
            var paginatedList = await _repository.Enjoins
                .Select(expression)
                .ToPaginatedListAsync(request.PageNumber, request.PageSize);
            return paginatedList;
        }
    }
}