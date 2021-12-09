﻿using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OnionArchitecture.Application.Interfaces.Repositories;
using OnionArchitecture.Infrastructure.Share.Results;

namespace OnionArchitecture.Application.Features.Privileges.Commands.Update
{
    public class UpdatePrivilegeCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        public int FunctionId { get; set; }
        public int RoleId { get; set; }
        public int EnjoinId { get; set; }

        public class UpdatePrivilegeCommandHandler : IRequestHandler<UpdatePrivilegeCommand, Result<int>>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IPrivilegeRepository _privilegeRepository;

            public UpdatePrivilegeCommandHandler(IPrivilegeRepository privilegeRepository, IUnitOfWork unitOfWork)
            {
                _privilegeRepository = privilegeRepository;
                _unitOfWork = unitOfWork;
            }

            public async Task<Result<int>> Handle(UpdatePrivilegeCommand command, CancellationToken cancellationToken)
            {
                var privilege = await _privilegeRepository.GetByIdAsync(command.Id);

                if (privilege == null)
                {
                    return await Result<int>.FailAsync($"Privilege Not Found.");
                }

                privilege.FunctionId = (command.FunctionId == 0) ? privilege.FunctionId : command.FunctionId;
                privilege.RoleId = (command.RoleId == 0) ? privilege.RoleId : command.RoleId;
                privilege.EnjoinId = (command.EnjoinId == 0) ? privilege.EnjoinId : command.EnjoinId;
                await _privilegeRepository.UpdateAsync(privilege);
                await _unitOfWork.Commit(cancellationToken);
                return await Result<int>.SuccessAsync(privilege.Id);
            }
        }
    }
}