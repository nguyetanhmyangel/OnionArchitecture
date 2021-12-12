using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using OnionArchitecture.Application.Interfaces.Repositories;
using OnionArchitecture.Domain.Entities;
using OnionArchitecture.Infrastructure.Share.Results;

namespace OnionArchitecture.Application.Features.AppCommandFunctions.Commands.Create
{
    public partial class CreateAppCommandFunctionCommand : IRequest<Result<int>>
    {
        public int AppCommandId { get; set; }
        public int FunctionId { get; set; }
    }

    public class CreateAppCommandFunctionCommandHandler : IRequestHandler<CreateAppCommandFunctionCommand, Result<int>>
    {
        private readonly IAppCommandFunctionRepository _appCommandFunctionRepository;
        private readonly IMapper _mapper;

        private IUnitOfWork _unitOfWork { get; set; }

        public CreateAppCommandFunctionCommandHandler(IAppCommandFunctionRepository appCommandFunctionRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _appCommandFunctionRepository = appCommandFunctionRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<int>> Handle(CreateAppCommandFunctionCommand request, CancellationToken cancellationToken)
        {
            var appCommandFunction = _mapper.Map<AppCommandFunction>(request);
            await _appCommandFunctionRepository.InsertAsync(appCommandFunction);
            await _unitOfWork.Commit(cancellationToken);
            return await Result<int>.SuccessAsync(appCommandFunction.Id);
        }
    }
}