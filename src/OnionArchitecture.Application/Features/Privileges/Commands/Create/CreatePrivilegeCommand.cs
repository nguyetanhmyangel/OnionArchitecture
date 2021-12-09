using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using OnionArchitecture.Application.Interfaces.Repositories;
using OnionArchitecture.Domain.Entities;
using OnionArchitecture.Infrastructure.Share.Results;

namespace OnionArchitecture.Application.Features.Privileges.Commands.Create
{
    public partial class CreatePrivilegeCommand : IRequest<Result<int>>
    {
        public int FunctionId { get; set; }
        public int RoleId { get; set; }
        public int EnjoinId { get; set; }
    }

    public class CreatePrivilegeCommandHandler : IRequestHandler<CreatePrivilegeCommand, Result<int>>
    {
        private readonly IPrivilegeRepository _privilegeRepository;
        private readonly IMapper _mapper;

        private IUnitOfWork _unitOfWork { get; set; }

        public CreatePrivilegeCommandHandler(IPrivilegeRepository privilegeRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _privilegeRepository = privilegeRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<int>> Handle(CreatePrivilegeCommand request, CancellationToken cancellationToken)
        {
            var privilege = _mapper.Map<Privilege>(request);
            await _privilegeRepository.InsertAsync(privilege);
            await _unitOfWork.Commit(cancellationToken);
            return await Result<int>.SuccessAsync(privilege.Id);
        }
    }
}