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

namespace OnionArchitecture.Application.Features.Functions.Queries.GetPage
{
    public class GetPageFunctionQuery : IRequest<PaginatedResult<GetPageFunctionResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public GetPageFunctionQuery(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }

    public class GetPageFunctionQueryHandler : IRequestHandler<GetPageFunctionQuery, PaginatedResult<GetPageFunctionResponse>>
    {
        private readonly IFunctionRepository _repository;

        public GetPageFunctionQueryHandler(IFunctionRepository repository)
        {
            _repository = repository;
        }

        public async Task<PaginatedResult<GetPageFunctionResponse>> Handle(GetPageFunctionQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Function, GetPageFunctionResponse>> expression = e => new GetPageFunctionResponse
            {
                Id = e.Id,
                Name = e.Name,
                Url = e.Url,
                SortOrder = e.SortOrder,
                ParentId = e.ParentId,
                Icon = e.Icon
            };
            var paginatedList = await _repository.Functions
                .Select(expression)
                .ToPaginatedListAsync(request.PageNumber, request.PageSize);
            return paginatedList;
        }
    }
}