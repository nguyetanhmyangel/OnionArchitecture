using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using OnionArchitecture.Application.Interfaces.Repositories;
using OnionArchitecture.Domain.Entities;
using OnionArchitecture.Infrastructure.Share.Results;

namespace OnionArchitecture.Application.Features.MySpaces.Commands.Create
{
    public partial class CreateMySpaceCommand : IRequest<Result<int>>
    {
        public int CategoryId { get; set; }

        public string Title { get; set; }

        public string SeoAlias { get; set; }

        public string Description { get; set; }

        public string Environment { get; set; }

        public string Problem { get; set; }

        public string StepToReproduce { get; set; }

        public string ErrorMessage { get; set; }

        public string Workaround { get; set; }

        public string Note { get; set; }

        public string Labels { get; set; }

        public int? NumberOfComments { get; set; }

        public int? NumberOfVotes { get; set; }

        public int? NumberOfReports { get; set; }
    }

    public class CreateMySpaceCommandHandler : IRequestHandler<CreateMySpaceCommand, Result<int>>
    {
        private readonly IMySpaceRepository _mySpaceRepository;
        private readonly IMapper _mapper;

        private IUnitOfWork _unitOfWork { get; set; }

        public CreateMySpaceCommandHandler(IMySpaceRepository mySpaceRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mySpaceRepository = mySpaceRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<int>> Handle(CreateMySpaceCommand request, CancellationToken cancellationToken)
        {
            var mySpace = _mapper.Map<MySpace>(request);
            await _mySpaceRepository.InsertAsync(mySpace);
            await _unitOfWork.Commit(cancellationToken);
            return await Result<int>.SuccessAsync(mySpace.Id);
        }
    }
}