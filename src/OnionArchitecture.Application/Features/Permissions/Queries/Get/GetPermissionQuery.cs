using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using OnionArchitecture.Application.Interfaces.Repositories;
using OnionArchitecture.Infrastructure.Share.Results;

namespace OnionArchitecture.Application.Features.Privileges.Queries.Get
{
    public class GetPermissionQuery : IRequest<Result<List<GetPermissionResponse>>>
    {
        public GetPermissionQuery()
        {
        }
    }

    public class GetPrivilegeQueryHandler : IRequestHandler<GetPermissionQuery, Result<List<GetPermissionResponse>>>
    {
        private readonly IAppPermissionRepository _privilegeRepository;
        private readonly IMapper _mapper;

        public GetPrivilegeQueryHandler(IAppPermissionRepository privilegeRepository, IMapper mapper)
        {
            _mapper = mapper;
            _privilegeRepository = privilegeRepository;
        }

        public async Task<Result<List<GetPermissionResponse>>> Handle(GetPermissionQuery request, CancellationToken cancellationToken)
        {
            var privilegeList = await _privilegeRepository.GetListAsync();
            var mappedPrivileges = _mapper.Map<List<GetPermissionResponse>>(privilegeList);
            return await Result<List<GetPermissionResponse>>.SuccessAsync(mappedPrivileges);
        }
    }
}