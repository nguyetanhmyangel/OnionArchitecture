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

namespace OnionArchitecture.Application.Features.Comments.Queries.GetPage
{
    public class GetPageCommentQuery : IRequest<PaginatedResult<GetPageCommentResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public GetPageCommentQuery(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }

    public class GetPageCommentQueryHandler : IRequestHandler<GetPageCommentQuery, PaginatedResult<GetPageCommentResponse>>
    {
        private readonly ICommentRepository _repository;

        public GetPageCommentQueryHandler(ICommentRepository repository)
        {
            _repository = repository;
        }

        public async Task<PaginatedResult<GetPageCommentResponse>> Handle(GetPageCommentQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Comment, GetPageCommentResponse>> expression = e => new GetPageCommentResponse
            {
                Id = e.Id,
                Content = e.Content,
                MyBaseId = e.MySpaceId
            };
            var paginatedList = await _repository.Comments
                .Select(expression)
                .ToPaginatedListAsync(request.PageNumber, request.PageSize);
            return paginatedList;
        }
    }
}