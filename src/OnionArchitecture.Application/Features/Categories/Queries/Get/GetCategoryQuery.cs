using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using OnionArchitecture.Application.Interfaces.Repositories;
using OnionArchitecture.Infrastructure.Share.Results;

namespace OnionArchitecture.Application.Features.Categories.Queries.Get
{
    public class GetCategoryQuery : IRequest<Result<List<GetCategoryResponse>>>
    {
        public GetCategoryQuery()
        {
        }
    }

    public class GetCategoryQueryHandler : IRequestHandler<GetCategoryQuery, Result<List<GetCategoryResponse>>>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public GetCategoryQueryHandler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<Result<List<GetCategoryResponse>>> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
        {
            var categoryList = await _categoryRepository.GetListAsync();
            var mappedCategories = _mapper.Map<List<GetCategoryResponse>>(categoryList);
            return await Result<List<GetCategoryResponse>>.SuccessAsync(mappedCategories);
        }
    }
}