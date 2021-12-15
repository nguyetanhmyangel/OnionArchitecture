using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using OnionArchitecture.Application.Interfaces.Repositories;
using OnionArchitecture.Domain.Entities;
using OnionArchitecture.Infrastructure.Share.Results;

namespace OnionArchitecture.Application.Features.AppPermissions.Commands.Create
{
    public partial class CreateAppPermissionCommand : IRequest<Result<int>>
    {
        public int FunctionId { get; set; }
        public int RoleId { get; set; }
        public int AppCommandId { get; set; }
    }

    public class CreatePermissionCommandHandler : IRequestHandler<CreateAppPermissionCommand, Result<int>>
    {
        private readonly IAppPermissionRepository _permissionRepository;
        private readonly IMapper _mapper;

        private IUnitOfWork _unitOfWork { get; set; }

        public CreatePermissionCommandHandler(IAppPermissionRepository permissionRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _permissionRepository = permissionRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<int>> Handle(CreateAppPermissionCommand request, CancellationToken cancellationToken)
        {
            var permission = _mapper.Map<AppPermission>(request);
            await _permissionRepository.InsertAsync(permission);
            await _unitOfWork.Commit(cancellationToken);
            return await Result<int>.SuccessAsync(permission.Id);
        }
    }
}