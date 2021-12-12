using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OnionArchitecture.Application.Interfaces.Repositories;
using OnionArchitecture.Infrastructure.Share.Results;

namespace OnionArchitecture.Application.Features.AppCommands.Commands.Delete
{
    public class DeleteAppCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }

        public class DeleteEnjoinCommandHandler : IRequestHandler<DeleteAppCommand, Result<int>>
        {
            private readonly IAppCommandRepository _appCommandRepository;
            private readonly IUnitOfWork _unitOfWork;

            public DeleteEnjoinCommandHandler(IAppCommandRepository appCommandRepository, IUnitOfWork unitOfWork)
            {
                _appCommandRepository = appCommandRepository;
                _unitOfWork = unitOfWork;
            }

            public async Task<Result<int>> Handle(DeleteAppCommand command, CancellationToken cancellationToken)
            {
                var appCommand = await _appCommandRepository.GetByIdAsync(command.Id);
                await _appCommandRepository.DeleteAsync(appCommand);
                await _unitOfWork.Commit(cancellationToken);
                return await Result<int>.SuccessAsync(appCommand.Id);
            }
        }
    }
}