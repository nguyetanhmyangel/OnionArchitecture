using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OnionArchitecture.Application.Interfaces.Repositories;
using OnionArchitecture.Infrastructure.Share.Results;

namespace OnionArchitecture.Application.Features.EnjoinFunctions.Commands.Update
{
    public class UpdateEnjoinFunctionCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        public int EnjoinId { get; set; }
        public int FunctionId { get; set; }

        public class UpdateEnjoinFunctionCommandHandler : IRequestHandler<UpdateEnjoinFunctionCommand, Result<int>>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IEnjoinFunctionRepository _enjoinFunctionRepository;

            public UpdateEnjoinFunctionCommandHandler(IEnjoinFunctionRepository enjoinFunctionRepository, IUnitOfWork unitOfWork)
            {
                _enjoinFunctionRepository = enjoinFunctionRepository;
                _unitOfWork = unitOfWork;
            }

            public async Task<Result<int>> Handle(UpdateEnjoinFunctionCommand command, CancellationToken cancellationToken)
            {
                var enjoinFunction = await _enjoinFunctionRepository.GetByIdAsync(command.Id);

                if (enjoinFunction == null)
                {
                    return await Result<int>.FailAsync($"EnjoinFunction Not Found.");
                }
                else
                {
                    enjoinFunction.EnjoinId = (command.EnjoinId == 0) ? enjoinFunction.EnjoinId : command.EnjoinId;
                    enjoinFunction.FunctionId = (command.FunctionId == 0) ? enjoinFunction.FunctionId : command.FunctionId;
                    await _enjoinFunctionRepository.UpdateAsync(enjoinFunction);
                    await _unitOfWork.Commit(cancellationToken);
                    return await Result<int>.SuccessAsync(enjoinFunction.Id);
                }
            }
        }
    }
}