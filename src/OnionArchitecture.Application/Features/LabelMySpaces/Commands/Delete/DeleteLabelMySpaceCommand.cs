using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OnionArchitecture.Application.Interfaces.Repositories;
using OnionArchitecture.Infrastructure.Share.Results;

namespace OnionArchitecture.Application.Features.LabelMySpaces.Commands.Delete
{
    public class DeleteLabelMySpaceCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }

        public class DeleteLabelMySpaceCommandHandler : IRequestHandler<DeleteLabelMySpaceCommand, Result<int>>
        {
            private readonly ILabelMySpaceRepository _labelMySpaceRepository;
            private readonly IUnitOfWork _unitOfWork;

            public DeleteLabelMySpaceCommandHandler(ILabelMySpaceRepository labelMySpaceRepository, IUnitOfWork unitOfWork)
            {
                _labelMySpaceRepository = labelMySpaceRepository;
                _unitOfWork = unitOfWork;
            }

            public async Task<Result<int>> Handle(DeleteLabelMySpaceCommand command, CancellationToken cancellationToken)
            {
                var labelMySpace = await _labelMySpaceRepository.GetByIdAsync(command.Id);
                await _labelMySpaceRepository.DeleteAsync(labelMySpace);
                await _unitOfWork.Commit(cancellationToken);
                return await Result<int>.SuccessAsync(labelMySpace.Id);
            }
        }
    }
}