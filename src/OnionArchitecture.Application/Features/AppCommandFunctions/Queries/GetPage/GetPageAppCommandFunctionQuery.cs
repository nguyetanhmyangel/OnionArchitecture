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

namespace OnionArchitecture.Application.Features.AppCommandFunctions.Queries.GetPage
{
    public class GetPageAppCommandFunctionQuery : IRequest<PaginatedResult<GetPageAppCommandFunctionResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public GetPageAppCommandFunctionQuery(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }

    public class GetPageEnjoinFunctionQueryHandler : IRequestHandler<GetPageAppCommandFunctionQuery, PaginatedResult<GetPageAppCommandFunctionResponse>>
    {
        private readonly IAppCommandFunctionRepository _repository;

        public GetPageEnjoinFunctionQueryHandler(IAppCommandFunctionRepository repository)
        {
            _repository = repository;
        }

        public async Task<PaginatedResult<GetPageAppCommandFunctionResponse>> Handle(GetPageAppCommandFunctionQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<AppCommandFunction, GetPageAppCommandFunctionResponse>> expression = e => new GetPageAppCommandFunctionResponse
            {
                Id = e.Id,
                AppCommandId = e.AppCommandId,
                FunctionId = e.FunctionId
            };
            var paginatedList = await _repository.AppCommandFunctions
                .Select(expression)
                .ToPaginatedListAsync(request.PageNumber, request.PageSize);
            return paginatedList;
        }
    }
}