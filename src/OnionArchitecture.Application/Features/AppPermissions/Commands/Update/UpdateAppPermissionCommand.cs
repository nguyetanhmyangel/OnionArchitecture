using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OnionArchitecture.Application.Interfaces.Repositories;
using OnionArchitecture.Infrastructure.Share.Results;

namespace OnionArchitecture.Application.Features.AppPermissions.Commands.Update
{
    public class UpdateAppPermissionCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        public int FunctionId { get; set; }
        public int RoleId { get; set; }
        public int AppCommandId { get; set; }

        public class UpdatePermissionCommandHandler : IRequestHandler<UpdateAppPermissionCommand, Result<int>>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IAppPermissionRepository _appPermissionRepository;

            public UpdatePermissionCommandHandler(IAppPermissionRepository appPermissionRepository, IUnitOfWork unitOfWork)
            {
                _appPermissionRepository = appPermissionRepository;
                _unitOfWork = unitOfWork;
            }

            public async Task<Result<int>> Handle(UpdateAppPermissionCommand command, CancellationToken cancellationToken)
            {
                var permission = await _appPermissionRepository.GetByIdAsync(command.Id);

                if (permission == null)
                {
                    return await Result<int>.FailAsync($"Privilege Not Found.");
                }

                permission.FunctionId = (command.FunctionId == 0) ? permission.FunctionId : command.FunctionId;
                permission.RoleId = (command.RoleId == 0) ? permission.RoleId : command.RoleId;
                permission.AppCommandId = (command.AppCommandId == 0) ? permission.AppCommandId : command.AppCommandId;
                await _appPermissionRepository.UpdateAsync(permission);
                await _unitOfWork.Commit(cancellationToken);
                return await Result<int>.SuccessAsync(permission.Id);
            }
        }
    }
}