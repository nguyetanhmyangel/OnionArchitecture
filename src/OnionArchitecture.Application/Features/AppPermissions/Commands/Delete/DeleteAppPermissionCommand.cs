using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OnionArchitecture.Application.Interfaces.Repositories;
using OnionArchitecture.Infrastructure.Share.Results;

namespace OnionArchitecture.Application.Features.AppPermissions.Commands.Delete
{
    public class DeleteAppPermissionCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }

        public class DeletePermissionCommandHandler : IRequestHandler<DeleteAppPermissionCommand, Result<int>>
        {
            private readonly IAppPermissionRepository _permissionRepository;
            private readonly IUnitOfWork _unitOfWork;

            public DeletePermissionCommandHandler(IAppPermissionRepository permissionRepository, IUnitOfWork unitOfWork)
            {
                _permissionRepository = permissionRepository;
                _unitOfWork = unitOfWork;
            }

            public async Task<Result<int>> Handle(DeleteAppPermissionCommand command, CancellationToken cancellationToken)
            {
                var permission = await _permissionRepository.GetByIdAsync(command.Id);
                await _permissionRepository.DeleteAsync(permission);
                await _unitOfWork.Commit(cancellationToken);
                return await Result<int>.SuccessAsync(permission.Id);
            }
        }
    }
}