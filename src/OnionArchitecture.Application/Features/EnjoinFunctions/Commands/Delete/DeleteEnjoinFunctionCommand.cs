using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OnionArchitecture.Application.Interfaces.Repositories;
using OnionArchitecture.Infrastructure.Share.Results;

namespace OnionArchitecture.Application.Features.EnjoinFunctions.Commands.Delete
{
    public class DeleteEnjoinFunctionCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }

        public class DeleteEnjoinFunctionCommandHandler : IRequestHandler<DeleteEnjoinFunctionCommand, Result<int>>
        {
            private readonly IEnjoinFunctionRepository _enjoinFunctionRepository;
            private readonly IUnitOfWork _unitOfWork;

            public DeleteEnjoinFunctionCommandHandler(IEnjoinFunctionRepository enjoinFunctionRepository, IUnitOfWork unitOfWork)
            {
                _enjoinFunctionRepository = enjoinFunctionRepository;
                _unitOfWork = unitOfWork;
            }

            public async Task<Result<int>> Handle(DeleteEnjoinFunctionCommand command, CancellationToken cancellationToken)
            {
                var enjoinFunction = await _enjoinFunctionRepository.GetByIdAsync(command.Id);
                await _enjoinFunctionRepository.DeleteAsync(enjoinFunction);
                await _unitOfWork.Commit(cancellationToken);
                return await Result<int>.SuccessAsync(enjoinFunction.Id);
            }
        }
    }
}