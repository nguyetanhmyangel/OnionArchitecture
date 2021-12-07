using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using OnionArchitecture.Application.Interfaces.Repositories;
using OnionArchitecture.Domain.Entities;
using OnionArchitecture.Infrastructure.Share.Results;

namespace OnionArchitecture.Application.Features.Functions.Commands.Create
{
    public partial class CreateFunctionCommand : IRequest<Result<int>>
    {
        public string Name { get; set; }

        public string Url { get; set; }

        public int SortOrder { get; set; }

        public string ParentId { get; set; }

        public string Icon { get; set; }
    }

    public class CreateFunctionCommandHandler : IRequestHandler<CreateFunctionCommand, Result<int>>
    {
        private readonly IFunctionRepository _functionRepository;
        private readonly IMapper _mapper;

        private IUnitOfWork _unitOfWork { get; set; }

        public CreateFunctionCommandHandler(IFunctionRepository functionRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _functionRepository = functionRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<int>> Handle(CreateFunctionCommand request, CancellationToken cancellationToken)
        {
            var function = _mapper.Map<Function>(request);
            await _functionRepository.InsertAsync(function);
            await _unitOfWork.Commit(cancellationToken);
            return await Result<int>.SuccessAsync(function.Id);
        }
    }
}