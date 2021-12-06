using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using OnionArchitecture.Application.Interfaces.Repositories;
using OnionArchitecture.Infrastructure.Share.Results;

namespace OnionArchitecture.Application.Features.Categories.Queries.GetById
{
    public class GetCategoryByIdQuery : IRequest<Result<GetCategoryByIdResponse>>
    {
        public int Id { get; set; }

        public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, Result<GetCategoryByIdResponse>>
        {
            private readonly ICategoryRepository _categoryCache;
            private readonly IMapper _mapper;

            public GetCategoryByIdQueryHandler(ICategoryRepository categoryCache, IMapper mapper)
            {
                _categoryCache = categoryCache;
                _mapper = mapper;
            }

            public async Task<Result<GetCategoryByIdResponse>> Handle(GetCategoryByIdQuery query, CancellationToken cancellationToken)
            {
                var category = await _categoryCache.GetByIdAsync(query.Id);
                var mappedCategory = _mapper.Map<GetCategoryByIdResponse>(category);
                return await Result<GetCategoryByIdResponse>.SuccessAsync(mappedCategory);
            }
        }
    }
}