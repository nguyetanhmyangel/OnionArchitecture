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

namespace OnionArchitecture.Application.Features.KnowledgeBases.Queries.GetPage
{
    public class GetPageKnowledgeBaseQuery : IRequest<PaginatedResult<GetPageKnowledgeBaseResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public GetPageKnowledgeBaseQuery(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }

    public class GetPageKnowledgeBaseQueryHandler : IRequestHandler<GetPageKnowledgeBaseQuery, PaginatedResult<GetPageKnowledgeBaseResponse>>
    {
        private readonly IKnowledgeBaseRepository _repository;

        public GetPageKnowledgeBaseQueryHandler(IKnowledgeBaseRepository repository)
        {
            _repository = repository;
        }

        public async Task<PaginatedResult<GetPageKnowledgeBaseResponse>> Handle(GetPageKnowledgeBaseQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<KnowledgeBase, GetPageKnowledgeBaseResponse>> expression = e => new GetPageKnowledgeBaseResponse
            {
                Id = e.Id,
                CategoryId = e.CategoryId,
                Title = e.Title,
                SeoAlias = e.SeoAlias,
                Description = e.Description,
                Environment = e.Environment,
                Problem = e.Problem,
                StepToReproduce = e.StepToReproduce,
                ErrorMessage = e.ErrorMessage,
                Workaround = e.Workaround,
                Note = e.Note,
                Labels = e.Labels,
                NumberOfComments = e.NumberOfComments,
                NumberOfVotes = e.NumberOfVotes,
                NumberOfReports = e.NumberOfReports
            };
            var paginatedList = await _repository.KnowledgeBases
                .Select(expression)
                .ToPaginatedListAsync(request.PageNumber, request.PageSize);
            return paginatedList;
        }
    }
}