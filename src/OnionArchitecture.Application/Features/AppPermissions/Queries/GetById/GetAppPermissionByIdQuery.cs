using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using OnionArchitecture.Application.Interfaces.Repositories;
using OnionArchitecture.Infrastructure.Share.Results;

namespace OnionArchitecture.Application.Features.AppPermissions.Queries.GetById
{
    public class GetAppPermissionByIdQuery : IRequest<Result<GetAppPermissionByIdResponse>>
    {
        public int Id { get; set; }

        public class GetPermissionByIdQueryHandler : IRequestHandler<GetAppPermissionByIdQuery, Result<GetAppPermissionByIdResponse>>
        {
            private readonly IAppPermissionRepository _appPermissionRepository;
            private readonly IMapper _mapper;

            public GetPermissionByIdQueryHandler(IAppPermissionRepository appPermissionRepository, IMapper mapper)
            {
                _appPermissionRepository = appPermissionRepository;
                _mapper = mapper;
            }

            public async Task<Result<GetAppPermissionByIdResponse>> Handle(GetAppPermissionByIdQuery query, CancellationToken cancellationToken)
            {
                var permission = await _appPermissionRepository.GetByIdAsync(query.Id);
                var mappedPermission = _mapper.Map<GetAppPermissionByIdResponse>(permission);
                return await Result<GetAppPermissionByIdResponse>.SuccessAsync(mappedPermission);
            }
        }
    }
}