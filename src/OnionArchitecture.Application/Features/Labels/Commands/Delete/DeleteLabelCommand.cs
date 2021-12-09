using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OnionArchitecture.Application.Interfaces.Repositories;
using OnionArchitecture.Infrastructure.Share.Results;

namespace OnionArchitecture.Application.Features.Labels.Commands.Delete
{
    public class DeleteLabelCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }

        public class DeleteLabelCommandHandler : IRequestHandler<DeleteLabelCommand, Result<int>>
        {
            private readonly ILabelRepository _labelRepository;
            private readonly IUnitOfWork _unitOfWork;

            public DeleteLabelCommandHandler(ILabelRepository labelRepository, IUnitOfWork unitOfWork)
            {
                _labelRepository = labelRepository;
                _unitOfWork = unitOfWork;
            }

            public async Task<Result<int>> Handle(DeleteLabelCommand command, CancellationToken cancellationToken)
            {
                var label = await _labelRepository.GetByIdAsync(command.Id);
                await _labelRepository.DeleteAsync(label);
                await _unitOfWork.Commit(cancellationToken);
                return await Result<int>.SuccessAsync(label.Id);
            }
        }
    }
}