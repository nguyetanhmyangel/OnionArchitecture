using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OnionArchitecture.Application.Extensions;
using OnionArchitecture.Application.Interfaces.Repositories;
using OnionArchitecture.Domain.Entities;
using OnionArchitecture.Infrastructure.Share.Results;

namespace OnionArchitecture.Application.Features.Categories.Queries.GetPage
{
    public class GetPageCategoryQuery : IRequest<PaginatedResult<GetPageCategoryResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public GetPageCategoryQuery(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }

    public class GetPageCategoryQueryHandler : IRequestHandler<GetPageCategoryQuery, PaginatedResult<GetPageCategoryResponse>>
    {
        private readonly ICategoryRepository _repository;

        public GetPageCategoryQueryHandler(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<PaginatedResult<GetPageCategoryResponse>> Handle(GetPageCategoryQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Category, GetPageCategoryResponse>> expression = e => new GetPageCategoryResponse
            {
                Id = e.Id,
                Name = e.Name,
                SeoAlias = e.SeoAlias,
                SeoDescription = e.SeoDescription,
                SortOrder = e.SortOrder,
                ParentId = e.ParentId,
                NumberOfTickets = e.NumberOfTickets
            };
            var paginatedList = await _repository.Categories
                .Select(expression)
                .ToPaginatedListAsync(request.PageNumber, request.PageSize);
            return paginatedList;
        }
    }
}