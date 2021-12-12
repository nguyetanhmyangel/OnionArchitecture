using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using OnionArchitecture.Application.Interfaces.Repositories;
using OnionArchitecture.Infrastructure.Share.Results;

namespace OnionArchitecture.Application.Features.Privileges.Queries.GetById
{
    public class GetPermissionByIdQuery : IRequest<Result<GetPermissionByIdResponse>>
    {
        public int Id { get; set; }

        public class GetPrivilegeByIdQueryHandler : IRequestHandler<GetPermissionByIdQuery, Result<GetPermissionByIdResponse>>
        {
            private readonly IAppPermissionRepository _privilegeCache;
            private readonly IMapper _mapper;

            public GetPrivilegeByIdQueryHandler(IAppPermissionRepository privilegeCache, IMapper mapper)
            {
                _privilegeCache = privilegeCache;
                _mapper = mapper;
            }

            public async Task<Result<GetPermissionByIdResponse>> Handle(GetPermissionByIdQuery query, CancellationToken cancellationToken)
            {
                var privilege = await _privilegeCache.GetByIdAsync(query.Id);
                var mappedPrivilege = _mapper.Map<GetPermissionByIdResponse>(privilege);
                return await Result<GetPermissionByIdResponse>.SuccessAsync(mappedPrivilege);
            }
        }
    }
}