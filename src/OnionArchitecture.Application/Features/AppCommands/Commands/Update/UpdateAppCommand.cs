using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OnionArchitecture.Application.Interfaces.Repositories;
using OnionArchitecture.Infrastructure.Share.Results;

namespace OnionArchitecture.Application.Features.AppCommands.Commands.Update
{
    public class UpdateAppCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public class UpdateAppCommandCommandHandler : IRequestHandler<UpdateAppCommand, Result<int>>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IAppCommandRepository _appCommandRepository;

            public UpdateAppCommandCommandHandler(IAppCommandRepository appCommandRepository, IUnitOfWork unitOfWork)
            {
                _appCommandRepository = appCommandRepository;
                _unitOfWork = unitOfWork;
            }

            public async Task<Result<int>> Handle(UpdateAppCommand command, CancellationToken cancellationToken)
            {
                var appCommand = await _appCommandRepository.GetByIdAsync(command.Id);

                if (appCommand == null)
                {
                    return await Result<int>.FailAsync($"AppCommand Not Found.");
                }
                else
                {
                    appCommand.Name = command.Name ?? appCommand.Name;
                    await _appCommandRepository.UpdateAsync(appCommand);
                    await _unitOfWork.Commit(cancellationToken);
                    return await Result<int>.SuccessAsync(appCommand.Id);
                }
            }
        }
    }
}