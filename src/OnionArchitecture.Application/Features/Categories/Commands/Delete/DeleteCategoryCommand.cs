using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OnionArchitecture.Application.Interfaces.Repositories;
using OnionArchitecture.Infrastructure.Share.Results;

namespace OnionArchitecture.Application.Features.Categories.Commands.Delete
{
    public class DeleteCategoryCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }

        public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, Result<int>>
        {
            private readonly ICategoryRepository _categoryRepository;
            private readonly IUnitOfWork _unitOfWork;

            public DeleteCategoryCommandHandler(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
            {
                _categoryRepository = categoryRepository;
                _unitOfWork = unitOfWork;
            }

            public async Task<Result<int>> Handle(DeleteCategoryCommand command, CancellationToken cancellationToken)
            {
                var category = await _categoryRepository.GetByIdAsync(command.Id);
                await _categoryRepository.DeleteAsync(category);
                await _unitOfWork.Commit(cancellationToken);
                return await Result<int>.SuccessAsync(category.Id);
            }
        }
    }
}