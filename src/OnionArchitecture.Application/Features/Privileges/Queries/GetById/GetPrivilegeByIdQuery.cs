using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using OnionArchitecture.Application.Interfaces.Repositories;
using OnionArchitecture.Infrastructure.Share.Results;

namespace OnionArchitecture.Application.Features.Privileges.Queries.GetById
{
    public class GetPrivilegeByIdQuery : IRequest<Result<GetPrivilegeByIdResponse>>
    {
        public int Id { get; set; }

        public class GetPrivilegeByIdQueryHandler : IRequestHandler<GetPrivilegeByIdQuery, Result<GetPrivilegeByIdResponse>>
        {
            private readonly IPrivilegeRepository _privilegeCache;
            private readonly IMapper _mapper;

            public GetPrivilegeByIdQueryHandler(IPrivilegeRepository privilegeCache, IMapper mapper)
            {
                _privilegeCache = privilegeCache;
                _mapper = mapper;
            }

            public async Task<Result<GetPrivilegeByIdResponse>> Handle(GetPrivilegeByIdQuery query, CancellationToken cancellationToken)
            {
                var privilege = await _privilegeCache.GetByIdAsync(query.Id);
                var mappedPrivilege = _mapper.Map<GetPrivilegeByIdResponse>(privilege);
                return await Result<GetPrivilegeByIdResponse>.SuccessAsync(mappedPrivilege);
            }
        }
    }
}