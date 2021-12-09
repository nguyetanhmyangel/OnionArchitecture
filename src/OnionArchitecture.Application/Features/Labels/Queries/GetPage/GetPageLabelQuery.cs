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

namespace OnionArchitecture.Application.Features.Labels.Queries.GetPage
{
    public class GetPageLabelQuery : IRequest<PaginatedResult<GetPageLabelResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public GetPageLabelQuery(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }

    public class GetPageLabelQueryHandler : IRequestHandler<GetPageLabelQuery, PaginatedResult<GetPageLabelResponse>>
    {
        private readonly ILabelRepository _repository;

        public GetPageLabelQueryHandler(ILabelRepository repository)
        {
            _repository = repository;
        }

        public async Task<PaginatedResult<GetPageLabelResponse>> Handle(GetPageLabelQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Label, GetPageLabelResponse>> expression = e => new GetPageLabelResponse
            {
                Id = e.Id,
                Name = e.Name
            };
            var paginatedList = await _repository.Labels
                .Select(expression)
                .ToPaginatedListAsync(request.PageNumber, request.PageSize);
            return paginatedList;
        }
    }
}