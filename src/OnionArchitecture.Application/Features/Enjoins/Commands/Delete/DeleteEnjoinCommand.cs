using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OnionArchitecture.Application.Interfaces.Repositories;
using OnionArchitecture.Infrastructure.Share.Results;

namespace OnionArchitecture.Application.Features.Enjoins.Commands.Delete
{
    public class DeleteEnjoinCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }

        public class DeleteEnjoinCommandHandler : IRequestHandler<DeleteEnjoinCommand, Result<int>>
        {
            private readonly IEnjoinRepository _enjoinRepository;
            private readonly IUnitOfWork _unitOfWork;

            public DeleteEnjoinCommandHandler(IEnjoinRepository enjoinRepository, IUnitOfWork unitOfWork)
            {
                _enjoinRepository = enjoinRepository;
                _unitOfWork = unitOfWork;
            }

            public async Task<Result<int>> Handle(DeleteEnjoinCommand command, CancellationToken cancellationToken)
            {
                var enjoin = await _enjoinRepository.GetByIdAsync(command.Id);
                await _enjoinRepository.DeleteAsync(enjoin);
                await _unitOfWork.Commit(cancellationToken);
                return await Result<int>.SuccessAsync(enjoin.Id);
            }
        }
    }
}