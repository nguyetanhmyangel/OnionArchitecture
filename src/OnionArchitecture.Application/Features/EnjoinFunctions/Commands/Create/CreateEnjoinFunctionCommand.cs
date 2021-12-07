using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using OnionArchitecture.Application.Interfaces.Repositories;
using OnionArchitecture.Domain.Entities;
using OnionArchitecture.Infrastructure.Share.Results;

namespace OnionArchitecture.Application.Features.EnjoinFunctions.Commands.Create
{
    public partial class CreateEnjoinFunctionCommand : IRequest<Result<int>>
    {
        public int EnjoinId { get; set; }
        public int FunctionId { get; set; }
    }

    public class CreateEnjoinFunctionCommandHandler : IRequestHandler<CreateEnjoinFunctionCommand, Result<int>>
    {
        private readonly IEnjoinFunctionRepository _enjoinFunctionRepository;
        private readonly IMapper _mapper;

        private IUnitOfWork _unitOfWork { get; set; }

        public CreateEnjoinFunctionCommandHandler(IEnjoinFunctionRepository enjoinFunctionRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _enjoinFunctionRepository = enjoinFunctionRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<int>> Handle(CreateEnjoinFunctionCommand request, CancellationToken cancellationToken)
        {
            var enjoinFunction = _mapper.Map<EnjoinFunction>(request);
            await _enjoinFunctionRepository.InsertAsync(enjoinFunction);
            await _unitOfWork.Commit(cancellationToken);
            return await Result<int>.SuccessAsync(enjoinFunction.Id);
        }
    }
}