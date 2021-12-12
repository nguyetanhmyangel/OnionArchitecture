using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OnionArchitecture.Application.Extensions;
using OnionArchitecture.Application.Features.Permissions.Queries.GetPage;
using OnionArchitecture.Application.Interfaces.Repositories;
using OnionArchitecture.Domain.Entities;
using OnionArchitecture.Infrastructure.Share.Results;

namespace OnionArchitecture.Application.Features.Privileges.Queries.GetPage
{
    public class GetPagePermissionQuery : IRequest<PaginatedResult<GetPagePermissionResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public GetPagePermissionQuery(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }

    public class GetPagePrivilegeQueryHandler : IRequestHandler<GetPagePermissionQuery, PaginatedResult<GetPagePermissionResponse>>
    {
        private readonly IAppPermissionRepository _repository;

        public GetPagePrivilegeQueryHandler(IAppPermissionRepository repository)
        {
            _repository = repository;
        }

        public async Task<PaginatedResult<GetPagePermissionResponse>> Handle(GetPagePermissionQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<AppPermission, GetPagePermissionResponse>> expression = e => new GetPagePermissionResponse
            {
                Id = e.Id,
                FunctionId = e.FunctionId,
                RoleId = e.RoleId,
                EnjoinId = e.AppCommandId
            };
            var paginatedList = await _repository.AppPermission
                .Select(expression)
                .ToPaginatedListAsync(request.PageNumber, request.PageSize);
            return paginatedList;
        }
    }
}