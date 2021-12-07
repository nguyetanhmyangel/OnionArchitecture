using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using OnionArchitecture.Application.Interfaces.Repositories;
using OnionArchitecture.Domain.Entities;
using OnionArchitecture.Infrastructure.Share.Results;

namespace OnionArchitecture.Application.Features.Enjoins.Commands.Create
{
    public partial class CreateEnjoinCommand : IRequest<Result<int>>
    {
        public string Name { get; set; }
    }

    public class CreateEnjoinCommandHandler : IRequestHandler<CreateEnjoinCommand, Result<int>>
    {
        private readonly IEnjoinRepository _enjoinRepository;
        private readonly IMapper _mapper;

        private IUnitOfWork _unitOfWork { get; set; }

        public CreateEnjoinCommandHandler(IEnjoinRepository enjoinRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _enjoinRepository = enjoinRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<int>> Handle(CreateEnjoinCommand request, CancellationToken cancellationToken)
        {
            var enjoin = _mapper.Map<Enjoin>(request);
            await _enjoinRepository.InsertAsync(enjoin);
            await _unitOfWork.Commit(cancellationToken);
            return await Result<int>.SuccessAsync(enjoin.Id);
        }
    }
}