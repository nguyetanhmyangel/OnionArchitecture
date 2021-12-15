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

namespace OnionArchitecture.Application.Features.LabelKnowledgeBases.Queries.GetPaged
{
    public class GetPageLabelKnowledgeBaseQuery : IRequest<PaginatedResult<GetPageLabelKnowledgeBaseResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public GetPageLabelKnowledgeBaseQuery(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }

    public class GetPageLabelKnowledgeBaseQueryHandler : IRequestHandler<GetPageLabelKnowledgeBaseQuery, PaginatedResult<GetPageLabelKnowledgeBaseResponse>>
    {
        private readonly ILabelKnowledgeBaseRepository _repository;

        public GetPageLabelKnowledgeBaseQueryHandler(ILabelKnowledgeBaseRepository repository)
        {
            _repository = repository;
        }

        public async Task<PaginatedResult<GetPageLabelKnowledgeBaseResponse>> Handle(GetPageLabelKnowledgeBaseQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<LabelKnowledgeBase, GetPageLabelKnowledgeBaseResponse>> expression = e => new GetPageLabelKnowledgeBaseResponse
            {
                Id = e.Id,
                KnowledgeBaseId = e.KnowledgeBaseId,
                LabelId = e.LabelId
            };
            var paginatedList = await _repository.LabelKnowledgeBases
                .Select(expression)
                .ToPaginatedListAsync(request.PageNumber, request.PageSize);
            return paginatedList;
        }
    }
}