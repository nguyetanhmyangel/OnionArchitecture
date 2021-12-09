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

namespace OnionArchitecture.Application.Features.EnjoinFunctions.Queries.GetPage
{
    public class GetPageEnjoinFunctionQuery : IRequest<PaginatedResult<GetPageEnjoinFunctionResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public GetPageEnjoinFunctionQuery(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }

    public class GetPageEnjoinFunctionQueryHandler : IRequestHandler<GetPageEnjoinFunctionQuery, PaginatedResult<GetPageEnjoinFunctionResponse>>
    {
        private readonly IEnjoinFunctionRepository _repository;

        public GetPageEnjoinFunctionQueryHandler(IEnjoinFunctionRepository repository)
        {
            _repository = repository;
        }

        public async Task<PaginatedResult<GetPageEnjoinFunctionResponse>> Handle(GetPageEnjoinFunctionQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<EnjoinFunction, GetPageEnjoinFunctionResponse>> expression = e => new GetPageEnjoinFunctionResponse
            {
                Id = e.Id,
                EnjoinId = e.EnjoinId,
                FunctionId = e.FunctionId
            };
            var paginatedList = await _repository.EnjoinFunctions
                .Select(expression)
                .ToPaginatedListAsync(request.PageNumber, request.PageSize);
            return paginatedList;
        }
    }
}