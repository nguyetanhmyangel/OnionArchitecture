using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using OnionArchitecture.Application.Interfaces.Repositories;
using OnionArchitecture.Domain.Entities;
using OnionArchitecture.Infrastructure.Share.Results;

namespace OnionArchitecture.Application.Features.AppCommands.Commands.Create
{
    public partial class CreateAppCommand : IRequest<Result<int>>
    {
        public string Name { get; set; }
    }

    public class CreateEnjoinCommandHandler : IRequestHandler<CreateAppCommand, Result<int>>
    {
        private readonly IAppCommandRepository _appCommandRepository;
        private readonly IMapper _mapper;

        private IUnitOfWork _unitOfWork { get; set; }

        public CreateEnjoinCommandHandler(IAppCommandRepository appCommandRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _appCommandRepository = appCommandRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<int>> Handle(CreateAppCommand request, CancellationToken cancellationToken)
        {
            var appCommand = _mapper.Map<AppCommand>(request);
            await _appCommandRepository.InsertAsync(appCommand);
            await _unitOfWork.Commit(cancellationToken);
            return await Result<int>.SuccessAsync(appCommand.Id);
        }
    }
}