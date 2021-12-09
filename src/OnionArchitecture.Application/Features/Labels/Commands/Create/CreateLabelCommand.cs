using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using OnionArchitecture.Application.Interfaces.Repositories;
using OnionArchitecture.Domain.Entities;
using OnionArchitecture.Infrastructure.Share.Results;

namespace OnionArchitecture.Application.Features.Labels.Commands.Create
{
    public partial class CreateLabelCommand : IRequest<Result<int>>
    {
        public string Name { get; set; }
    }

    public class CreateLabelCommandHandler : IRequestHandler<CreateLabelCommand, Result<int>>
    {
        private readonly ILabelRepository _labelRepository;
        private readonly IMapper _mapper;

        private IUnitOfWork _unitOfWork { get; set; }

        public CreateLabelCommandHandler(ILabelRepository labelRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _labelRepository = labelRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<int>> Handle(CreateLabelCommand request, CancellationToken cancellationToken)
        {
            var label = _mapper.Map<Label>(request);
            await _labelRepository.InsertAsync(label);
            await _unitOfWork.Commit(cancellationToken);
            return await Result<int>.SuccessAsync(label.Id);
        }
    }
}