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

namespace OnionArchitecture.Application.Features.LabelMySpaces.Queries.GetPaged
{
    public class GetPageLabelMySpaceQuery : IRequest<PaginatedResult<GetPageLabelMySpaceResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public GetPageLabelMySpaceQuery(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }

    public class GetPageLabelMySpaceQueryHandler : IRequestHandler<GetPageLabelMySpaceQuery, PaginatedResult<GetPageLabelMySpaceResponse>>
    {
        private readonly ILabelMySpaceRepository _repository;

        public GetPageLabelMySpaceQueryHandler(ILabelMySpaceRepository repository)
        {
            _repository = repository;
        }

        public async Task<PaginatedResult<GetPageLabelMySpaceResponse>> Handle(GetPageLabelMySpaceQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<LabelMySpace, GetPageLabelMySpaceResponse>> expression = e => new GetPageLabelMySpaceResponse
            {
                Id = e.Id,
                MySpaceId = e.MySpaceId,
                LabelId = e.LabelId
            };
            var paginatedList = await _repository.LabelMySpaces
                .Select(expression)
                .ToPaginatedListAsync(request.PageNumber, request.PageSize);
            return paginatedList;
        }
    }
}