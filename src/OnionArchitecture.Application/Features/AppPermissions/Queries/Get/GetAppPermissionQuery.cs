using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using OnionArchitecture.Application.Interfaces.Repositories;
using OnionArchitecture.Infrastructure.Share.Results;

namespace OnionArchitecture.Application.Features.AppPermissions.Queries.Get
{
    public class GetAppPermissionQuery : IRequest<Result<List<GetAppPermissionResponse>>>
    {
        public GetAppPermissionQuery()
        {
        }
    }

    public class GetPermissionQueryHandler : IRequestHandler<GetAppPermissionQuery, Result<List<GetAppPermissionResponse>>>
    {
        private readonly IAppPermissionRepository _permissionRepository;
        private readonly IMapper _mapper;

        public GetPermissionQueryHandler(IAppPermissionRepository permissionRepository, IMapper mapper)
        {
            _mapper = mapper;
            _permissionRepository = permissionRepository;
        }

        public async Task<Result<List<GetAppPermissionResponse>>> Handle(GetAppPermissionQuery request, CancellationToken cancellationToken)
        {
            var permissionList = await _permissionRepository.GetListAsync();
            var mappedPermissions = _mapper.Map<List<GetAppPermissionResponse>>(permissionList);
            return await Result<List<GetAppPermissionResponse>>.SuccessAsync(mappedPermissions);
        }
    }
}