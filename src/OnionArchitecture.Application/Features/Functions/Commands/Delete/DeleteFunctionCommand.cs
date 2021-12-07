using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OnionArchitecture.Application.Interfaces.Repositories;
using OnionArchitecture.Infrastructure.Share.Results;

namespace OnionArchitecture.Application.Features.Functions.Commands.Delete
{
    public class DeleteFunctionCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }

        public class DeleteFunctionCommandHandler : IRequestHandler<DeleteFunctionCommand, Result<int>>
        {
            private readonly IFunctionRepository _functionRepository;
            private readonly IUnitOfWork _unitOfWork;

            public DeleteFunctionCommandHandler(IFunctionRepository functionRepository, IUnitOfWork unitOfWork)
            {
                _functionRepository = functionRepository;
                _unitOfWork = unitOfWork;
            }

            public async Task<Result<int>> Handle(DeleteFunctionCommand command, CancellationToken cancellationToken)
            {
                var function = await _functionRepository.GetByIdAsync(command.Id);
                await _functionRepository.DeleteAsync(function);
                await _unitOfWork.Commit(cancellationToken);
                return await Result<int>.SuccessAsync(function.Id);
            }
        }
    }
}