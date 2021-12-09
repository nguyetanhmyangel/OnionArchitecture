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

namespace OnionArchitecture.Application.Features.Votes.Queries.GetPage
{
    public class GetPageVoteQuery : IRequest<PaginatedResult<GetPageVoteResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public GetPageVoteQuery(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }

    public class GetPageVoteQueryHandler : IRequestHandler<GetPageVoteQuery, PaginatedResult<GetPageVoteResponse>>
    {
        private readonly IVoteRepository _repository;

        public GetPageVoteQueryHandler(IVoteRepository repository)
        {
            _repository = repository;
        }

        public async Task<PaginatedResult<GetPageVoteResponse>> Handle(GetPageVoteQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Vote, GetPageVoteResponse>> expression = e => new GetPageVoteResponse
            {
                Id = e.Id,
                MySpaceId = e.MySpaceId
            };
            var paginatedList = await _repository.Votes
                .Select(expression)
                .ToPaginatedListAsync(request.PageNumber, request.PageSize);
            return paginatedList;
        }
    }
}