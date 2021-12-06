using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OnionArchitecture.Application.Interfaces.Repositories;
using OnionArchitecture.Domain.Entities;
using OnionArchitecture.Infrastructure.Share.Results;

namespace OnionArchitecture.Application.Features.Categories.Commands.Update
{
    public class UpdateCategoryCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SeoAlias { get; set; }
        public string SeoDescription { get; set; }
        public int SortOrder { get; set; }
        public int? ParentId { get; set; }
        public int? NumberOfTickets { get; set; }

        public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, Result<int>>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly ICategoryRepository _categoryRepository;

            public UpdateCategoryCommandHandler(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
            {
                _categoryRepository = categoryRepository;
                _unitOfWork = unitOfWork;
            }

            public async Task<Result<int>> Handle(UpdateCategoryCommand command, CancellationToken cancellationToken)
            {
                var category = await _categoryRepository.GetByIdAsync(command.Id);

                if (category == null)
                {
                    return await Result<int>.FailAsync($"Category Not Found.");
                }
                else
                {
                    category.Name = command.Name ?? category.Name;
                    category.SeoAlias = command.SeoAlias ?? category.SeoAlias;
                    category.SeoDescription = command.SeoDescription ?? category.SeoDescription;
                    if (command.SortOrder == 0)
                        category.SortOrder = category.SortOrder;
                    else
                        category.SortOrder = command.SortOrder;
                    category.FileType = command.FileName ?? category.FileName;
                    category.KnowledgeBaseId = (command.KnowledgeBaseId == 0) ? category.KnowledgeBaseId : command.KnowledgeBaseId;
                    await _categoryRepository.UpdateAsync(category);
                    await _unitOfWork.Commit(cancellationToken);
                    return await Result<int>.SuccessAsync(category.Id);
                }
            }
        }
    }
}