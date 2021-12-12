using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OnionArchitecture.Application.Interfaces.Repositories;
using OnionArchitecture.Infrastructure.Share.Results;

namespace OnionArchitecture.Application.Features.AppCommandFunctions.Commands.Update
{
    public class UpdateAppCommandFunctionCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        public int AppCommandId { get; set; }
        public int FunctionId { get; set; }

        public class UpdateEnjoinFunctionCommandHandler : IRequestHandler<UpdateAppCommandFunctionCommand, Result<int>>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IAppCommandFunctionRepository _apCommandFunctionRepository;

            public UpdateEnjoinFunctionCommandHandler(IAppCommandFunctionRepository appCommandFunctionRepository, IUnitOfWork unitOfWork)
            {
                _apCommandFunctionRepository = appCommandFunctionRepository;
                _unitOfWork = unitOfWork;
            }

            public async Task<Result<int>> Handle(UpdateAppCommandFunctionCommand command, CancellationToken cancellationToken)
            {
                var appCommandFunction = await _apCommandFunctionRepository.GetByIdAsync(command.Id);

                if (appCommandFunction == null)
                {
                    return await Result<int>.FailAsync($"EnjoinFunction Not Found.");
                }
                else
                {
                    appCommandFunction.AppCommandId = (command.AppCommandId == 0) ? appCommandFunction.AppCommandId : command.AppCommandId;
                    appCommandFunction.FunctionId = (command.FunctionId == 0) ? appCommandFunction.FunctionId : command.FunctionId;
                    await _apCommandFunctionRepository.UpdateAsync(appCommandFunction);
                    await _unitOfWork.Commit(cancellationToken);
                    return await Result<int>.SuccessAsync(appCommandFunction.Id);
                }
            }
        }
    }
}