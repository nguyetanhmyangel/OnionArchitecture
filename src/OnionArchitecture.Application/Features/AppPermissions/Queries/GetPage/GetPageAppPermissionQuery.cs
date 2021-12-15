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

namespace OnionArchitecture.Application.Features.AppPermissions.Queries.GetPage
{
    public class GetPageAppPermissionQuery : IRequest<PaginatedResult<GetAppPagePermissionResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public GetPageAppPermissionQuery(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }

    public class GetPagePermissionQueryHandler : IRequestHandler<GetPageAppPermissionQuery, PaginatedResult<GetAppPagePermissionResponse>>
    {
        private readonly IAppPermissionRepository _repository;

        public GetPagePermissionQueryHandler(IAppPermissionRepository repository)
        {
            _repository = repository;
        }

        public async Task<PaginatedResult<GetAppPagePermissionResponse>> Handle(GetPageAppPermissionQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<AppPermission, GetAppPagePermissionResponse>> expression = e => new GetAppPagePermissionResponse
            {
                Id = e.Id,
                FunctionId = e.FunctionId,
                RoleId = e.RoleId,
                AppCommandId = e.AppCommandId
            };
            var paginatedList = await _repository.AppPermission
                .Select(expression)
                .ToPaginatedListAsync(request.PageNumber, request.PageSize);
            return paginatedList;
        }
    }
}