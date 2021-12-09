using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OnionArchitecture.Application.Interfaces.Repositories;
using OnionArchitecture.Infrastructure.Share.Results;

namespace OnionArchitecture.Application.Features.Privileges.Commands.Delete
{
    public class DeletePrivilegeCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }

        public class DeletePrivilegeCommandHandler : IRequestHandler<DeletePrivilegeCommand, Result<int>>
        {
            private readonly IPrivilegeRepository _privilegeRepository;
            private readonly IUnitOfWork _unitOfWork;

            public DeletePrivilegeCommandHandler(IPrivilegeRepository privilegeRepository, IUnitOfWork unitOfWork)
            {
                _privilegeRepository = privilegeRepository;
                _unitOfWork = unitOfWork;
            }

            public async Task<Result<int>> Handle(DeletePrivilegeCommand command, CancellationToken cancellationToken)
            {
                var privilege = await _privilegeRepository.GetByIdAsync(command.Id);
                await _privilegeRepository.DeleteAsync(privilege);
                await _unitOfWork.Commit(cancellationToken);
                return await Result<int>.SuccessAsync(privilege.Id);
            }
        }
    }
}