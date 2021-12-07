using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using OnionArchitecture.Application.Interfaces.Repositories;
using OnionArchitecture.Domain.Entities;
using OnionArchitecture.Infrastructure.Share.Results;

namespace OnionArchitecture.Application.Features.LabelMySpaces.Commands.Create
{
    public partial class CreateLabelMySpaceCommand : IRequest<Result<int>>
    {
        public int MySpaceId { get; set; }
        public int LabelId { get; set; }
    }

    public class CreateLabelMySpaceCommandHandler : IRequestHandler<CreateLabelMySpaceCommand, Result<int>>
    {
        private readonly ILabelMySpaceRepository _labelMySpaceRepository;
        private readonly IMapper _mapper;

        private IUnitOfWork _unitOfWork { get; set; }

        public CreateLabelMySpaceCommandHandler(ILabelMySpaceRepository labelMySpaceRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _labelMySpaceRepository = labelMySpaceRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<int>> Handle(CreateLabelMySpaceCommand request, CancellationToken cancellationToken)
        {
            var labelMySpace = _mapper.Map<LabelMySpace>(request);
            await _labelMySpaceRepository.InsertAsync(labelMySpace);
            await _unitOfWork.Commit(cancellationToken);
            return await Result<int>.SuccessAsync(labelMySpace.Id);
        }
    }
}