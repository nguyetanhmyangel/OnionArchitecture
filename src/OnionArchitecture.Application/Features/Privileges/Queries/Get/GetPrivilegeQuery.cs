using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using OnionArchitecture.Application.Interfaces.Repositories;
using OnionArchitecture.Infrastructure.Share.Results;

namespace OnionArchitecture.Application.Features.Privileges.Queries.Get
{
    public class GetPrivilegeQuery : IRequest<Result<List<GetPrivilegeResponse>>>
    {
        public GetPrivilegeQuery()
        {
        }
    }

    public class GetPrivilegeQueryHandler : IRequestHandler<GetPrivilegeQuery, Result<List<GetPrivilegeResponse>>>
    {
        private readonly IPrivilegeRepository _privilegeRepository;
        private readonly IMapper _mapper;

        public GetPrivilegeQueryHandler(IPrivilegeRepository privilegeRepository, IMapper mapper)
        {
            _mapper = mapper;
            _privilegeRepository = privilegeRepository;
        }

        public async Task<Result<List<GetPrivilegeResponse>>> Handle(GetPrivilegeQuery request, CancellationToken cancellationToken)
        {
            var privilegeList = await _privilegeRepository.GetListAsync();
            var mappedPrivileges = _mapper.Map<List<GetPrivilegeResponse>>(privilegeList);
            return await Result<List<GetPrivilegeResponse>>.SuccessAsync(mappedPrivileges);
        }
    }
}