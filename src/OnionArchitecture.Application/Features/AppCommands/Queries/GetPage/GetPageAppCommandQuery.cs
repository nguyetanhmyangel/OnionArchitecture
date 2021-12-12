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

namespace OnionArchitecture.Application.Features.AppCommands.Queries.GetPage
{
    public class GetPageAppCommandQuery : IRequest<PaginatedResult<GetPageAppCommandResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public GetPageAppCommandQuery(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }

    public class GetPageEnjoinQueryHandler : IRequestHandler<GetPageAppCommandQuery, PaginatedResult<GetPageAppCommandResponse>>
    {
        private readonly IAppCommandRepository _repository;

        public GetPageEnjoinQueryHandler(IAppCommandRepository repository)
        {
            _repository = repository;
        }

        public async Task<PaginatedResult<GetPageAppCommandResponse>> Handle(GetPageAppCommandQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<AppCommand, GetPageAppCommandResponse>> expression = e => new GetPageAppCommandResponse
            {
                Id = e.Id,
                Name = e.Name
            };
            var paginatedList = await _repository.AppCommands
                .Select(expression)
                .ToPaginatedListAsync(request.PageNumber, request.PageSize);
            return paginatedList;
        }
    }
}