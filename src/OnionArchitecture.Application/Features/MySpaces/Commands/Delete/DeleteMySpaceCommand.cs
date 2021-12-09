using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OnionArchitecture.Application.Interfaces.Repositories;
using OnionArchitecture.Infrastructure.Share.Results;

namespace OnionArchitecture.Application.Features.MySpaces.Commands.Delete
{
    public class DeleteMySpaceCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }

        public class DeleteMySpaceCommandHandler : IRequestHandler<DeleteMySpaceCommand, Result<int>>
        {
            private readonly IMySpaceRepository _mySpaceRepository;
            private readonly IUnitOfWork _unitOfWork;

            public DeleteMySpaceCommandHandler(IMySpaceRepository mySpaceRepository, IUnitOfWork unitOfWork)
            {
                _mySpaceRepository = mySpaceRepository;
                _unitOfWork = unitOfWork;
            }

            public async Task<Result<int>> Handle(DeleteMySpaceCommand command, CancellationToken cancellationToken)
            {
                var mySpace = await _mySpaceRepository.GetByIdAsync(command.Id);
                await _mySpaceRepository.DeleteAsync(mySpace);
                await _unitOfWork.Commit(cancellationToken);
                return await Result<int>.SuccessAsync(mySpace.Id);
            }
        }
    }
}