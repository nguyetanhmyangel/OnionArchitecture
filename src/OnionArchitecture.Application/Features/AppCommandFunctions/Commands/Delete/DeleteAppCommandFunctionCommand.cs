using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OnionArchitecture.Application.Interfaces.Repositories;
using OnionArchitecture.Infrastructure.Share.Results;

namespace OnionArchitecture.Application.Features.AppCommandFunctions.Commands.Delete
{
    public class DeleteAppCommandFunctionCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }

        public class DeleteEnjoinFunctionCommandHandler : IRequestHandler<DeleteAppCommandFunctionCommand, Result<int>>
        {
            private readonly IAppCommandFunctionRepository _appCommandFunctionRepository;
            private readonly IUnitOfWork _unitOfWork;

            public DeleteEnjoinFunctionCommandHandler(IAppCommandFunctionRepository appCommandFunctionRepository, IUnitOfWork unitOfWork)
            {
                _appCommandFunctionRepository = appCommandFunctionRepository;
                _unitOfWork = unitOfWork;
            }

            public async Task<Result<int>> Handle(DeleteAppCommandFunctionCommand command, CancellationToken cancellationToken)
            {
                var appCommandFunction = await _appCommandFunctionRepository.GetByIdAsync(command.Id);
                await _appCommandFunctionRepository.DeleteAsync(appCommandFunction);
                await _unitOfWork.Commit(cancellationToken);
                return await Result<int>.SuccessAsync(appCommandFunction.Id);
            }
        }
    }
}