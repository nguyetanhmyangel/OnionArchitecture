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

namespace OnionArchitecture.Application.Features.Privileges.Queries.GetPage
{
    public class GetPagePrivilegeQuery : IRequest<PaginatedResult<GetPagePrivilegeResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public GetPagePrivilegeQuery(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }

    public class GetPagePrivilegeQueryHandler : IRequestHandler<GetPagePrivilegeQuery, PaginatedResult<GetPagePrivilegeResponse>>
    {
        private readonly IPrivilegeRepository _repository;

        public GetPagePrivilegeQueryHandler(IPrivilegeRepository repository)
        {
            _repository = repository;
        }

        public async Task<PaginatedResult<GetPagePrivilegeResponse>> Handle(GetPagePrivilegeQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Privilege, GetPagePrivilegeResponse>> expression = e => new GetPagePrivilegeResponse
            {
                Id = e.Id,
                FunctionId = e.FunctionId,
                RoleId = e.RoleId,
                EnjoinId = e.EnjoinId
            };
            var paginatedList = await _repository.Privileges
                .Select(expression)
                .ToPaginatedListAsync(request.PageNumber, request.PageSize);
            return paginatedList;
        }
    }
}