using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OnionArchitecture.Application.Interfaces.Repositories;
using OnionArchitecture.Infrastructure.Share.Results;

namespace OnionArchitecture.Application.Features.Enjoins.Commands.Update
{
    public class UpdateEnjoinCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public class UpdateEnjoinCommandHandler : IRequestHandler<UpdateEnjoinCommand, Result<int>>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IEnjoinRepository _enjoinRepository;

            public UpdateEnjoinCommandHandler(IEnjoinRepository enjoinRepository, IUnitOfWork unitOfWork)
            {
                _enjoinRepository = enjoinRepository;
                _unitOfWork = unitOfWork;
            }

            public async Task<Result<int>> Handle(UpdateEnjoinCommand command, CancellationToken cancellationToken)
            {
                var enjoin = await _enjoinRepository.GetByIdAsync(command.Id);

                if (enjoin == null)
                {
                    return await Result<int>.FailAsync($"Enjoin Not Found.");
                }
                else
                {
                    enjoin.Name = command.Name ?? enjoin.Name;
                    await _enjoinRepository.UpdateAsync(enjoin);
                    await _unitOfWork.Commit(cancellationToken);
                    return await Result<int>.SuccessAsync(enjoin.Id);
                }
            }
        }
    }
}