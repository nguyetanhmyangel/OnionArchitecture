using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using OnionArchitecture.Application.Interfaces.Repositories;
using OnionArchitecture.Domain.Entities;
using OnionArchitecture.Infrastructure.Share.Results;

namespace OnionArchitecture.Application.Features.Categories.Commands.Create
{
    public partial class CreateCategoryCommand : IRequest<Result<int>>
    {
        public string Name { get; set; }
        public string SeoAlias { get; set; }
        public string SeoDescription { get; set; }
        public int SortOrder { get; set; }
        public int? ParentId { get; set; }
        public int? NumberOfTickets { get; set; }
    }

    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Result<int>>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        private IUnitOfWork _unitOfWork { get; set; }

        public CreateCategoryCommandHandler(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<int>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = _mapper.Map<Category>(request);
            await _categoryRepository.InsertAsync(category);
            await _unitOfWork.Commit(cancellationToken);
            return await Result<int>.SuccessAsync(category.Id);
        }
    }
}