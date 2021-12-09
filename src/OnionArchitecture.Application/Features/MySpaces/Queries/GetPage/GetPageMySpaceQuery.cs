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

namespace OnionArchitecture.Application.Features.MySpaces.Queries.GetPage
{
    public class GetPageMySpaceQuery : IRequest<PaginatedResult<GetPageMySpaceResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public GetPageMySpaceQuery(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }

    public class GetPageMySpaceQueryHandler : IRequestHandler<GetPageMySpaceQuery, PaginatedResult<GetPageMySpaceResponse>>
    {
        private readonly IMySpaceRepository _repository;

        public GetPageMySpaceQueryHandler(IMySpaceRepository repository)
        {
            _repository = repository;
        }

        public async Task<PaginatedResult<GetPageMySpaceResponse>> Handle(GetPageMySpaceQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<MySpace, GetPageMySpaceResponse>> expression = e => new GetPageMySpaceResponse
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
            var paginatedList = await _repository.MySpaces
                .Select(expression)
                .ToPaginatedListAsync(request.PageNumber, request.PageSize);
            return paginatedList;
        }
    }
}