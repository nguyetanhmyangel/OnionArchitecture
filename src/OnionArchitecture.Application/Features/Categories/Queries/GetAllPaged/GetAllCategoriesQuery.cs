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

namespace OnionArchitecture.Application.Features.Categories.Queries.GetAllPaged
{
    public class GetAllCategoriesQuery : IRequest<PaginatedResult<GetAllCategoriesResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public GetAllCategoriesQuery(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }

    public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, PaginatedResult<GetAllCategoriesResponse>>
    {
        private readonly ICategoryRepository _repository;

        public GetAllCategoriesQueryHandler(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<PaginatedResult<GetAllCategoriesResponse>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Category, GetAllCategoriesResponse>> expression = e => new GetAllCategoriesResponse
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