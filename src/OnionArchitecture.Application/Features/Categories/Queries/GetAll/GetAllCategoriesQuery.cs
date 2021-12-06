using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using OnionArchitecture.Application.Interfaces.Repositories;
using OnionArchitecture.Infrastructure.Share.Results;

namespace OnionArchitecture.Application.Features.Categories.Queries.GetAll
{
    public class GetAllCategoriesQuery : IRequest<Result<List<GetAllCategoriesResponse>>>
    {
        public GetAllCategoriesQuery()
        {
        }
    }

    public class GetAllCategoriesCachedQueryHandler : IRequestHandler<GetAllCategoriesQuery, Result<List<GetAllCategoriesResponse>>>
    {
        private readonly ICategoryRepository _categoryCache;
        private readonly IMapper _mapper;

        public GetAllCategoriesCachedQueryHandler(ICategoryRepository categoryCache, IMapper mapper)
        {
            _categoryCache = categoryCache;
            _mapper = mapper;
        }

        public async Task<Result<List<GetAllCategoriesResponse>>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            var categoriesList = await _categoryCache.GetListAsync();
            var mappedCategories = _mapper.Map<List<GetAllCategoriesResponse>>(categoriesList);
            return await Result<List<GetAllCategoriesResponse>>.SuccessAsync(mappedCategories);
        }
    }
}