using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OnionArchitecture.Application.Interfaces.Repositories;
using OnionArchitecture.Infrastructure.Share.Results;

namespace OnionArchitecture.Application.Features.Labels.Commands.Update
{
    public class UpdateLabelCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public class UpdateLabelCommandHandler : IRequestHandler<UpdateLabelCommand, Result<int>>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly ILabelRepository _labelRepository;

            public UpdateLabelCommandHandler(ILabelRepository labelRepository, IUnitOfWork unitOfWork)
            {
                _labelRepository = labelRepository;
                _unitOfWork = unitOfWork;
            }

            public async Task<Result<int>> Handle(UpdateLabelCommand command, CancellationToken cancellationToken)
            {
                var product = await _labelRepository.GetByIdAsync(command.Id);

                if (product == null)
                {
                    return await Result<int>.FailAsync($"Product Not Found.");
                }
                else
                {
                    product.Name = command.Name ?? product.Name;
                    await _labelRepository.UpdateAsync(product);
                    await _unitOfWork.Commit(cancellationToken);
                    return await Result<int>.SuccessAsync(product.Id);
                }
            }
        }
    }
}