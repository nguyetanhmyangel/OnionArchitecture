using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OnionArchitecture.Application.Interfaces.Repositories;
using OnionArchitecture.Infrastructure.Share.Results;

namespace OnionArchitecture.Application.Features.LabelMySpaces.Commands.Update
{
    public class UpdateLabelMySpaceCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        public int MySpaceId { get; set; }
        public int LabelId { get; set; }

        public class UpdateLabelMySpaceCommandHandler : IRequestHandler<UpdateLabelMySpaceCommand, Result<int>>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly ILabelMySpaceRepository _labelMySpaceRepository;

            public UpdateLabelMySpaceCommandHandler(ILabelMySpaceRepository labelMySpaceRepository, IUnitOfWork unitOfWork)
            {
                _labelMySpaceRepository = labelMySpaceRepository;
                _unitOfWork = unitOfWork;
            }

            public async Task<Result<int>> Handle(UpdateLabelMySpaceCommand command, CancellationToken cancellationToken)
            {
                var product = await _labelMySpaceRepository.GetByIdAsync(command.Id);

                if (product == null)
                {
                    return await Result<int>.FailAsync($"Not Found.");
                }
                else
                {
                    product.MySpaceId = (command.MySpaceId == 0) ? product.MySpaceId : command.MySpaceId;
                    product.LabelId = (command.LabelId == 0) ? product.LabelId : command.LabelId;

                    await _labelMySpaceRepository.UpdateAsync(product);
                    await _unitOfWork.Commit(cancellationToken);
                    return await Result<int>.SuccessAsync(product.Id);
                }
            }
        }
    }
}