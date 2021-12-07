using AutoMapper;
using MediatR;
using OnionArchitecture.Application.Interfaces.Repositories;
using OnionArchitecture.Infrastructure.Share.Results;
using System.Threading;
using System.Threading.Tasks;

namespace OnionArchitecture.Application.Features.Categories.Queries.GetById
{
    public class GetCategoryByIdQuery : IRequest<Result<GetCategoryByIdResponse>>
    {
        public int Id { get; set; }

        public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, Result<GetCategoryByIdResponse>>
        {
            private readonly ICategoryRepository _categoryRepository;
            private readonly IMapper _mapper;

            public GetCategoryByIdQueryHandler(ICategoryRepository categoryRepository, IMapper mapper)
            {
                _categoryRepository = categoryRepository;
                _mapper = mapper;
            }

            public async Task<Result<GetCategoryByIdResponse>> Handle(GetCategoryByIdQuery query, CancellationToken cancellationToken)
            {
                var category = await _categoryRepository.GetByIdAsync(query.Id);
                var mappedCategory = _mapper.Map<GetCategoryByIdResponse>(category);
                return await Result<GetCategoryByIdResponse>.SuccessAsync(mappedCategory);
            }
        }
    }
}