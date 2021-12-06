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

namespace OnionArchitecture.Application.Features.Attachments.Queries.GetAllPaged
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

    public class GetAllAttachmentsQueryHandler : IRequestHandler<GetAllCategoriesQuery, PaginatedResult<GetAllCategoriesResponse>>
    {
        private readonly IAttachmentRepository _repository;

        public GetAllAttachmentsQueryHandler(IAttachmentRepository repository)
        {
            _repository = repository;
        }

        public async Task<PaginatedResult<GetAllCategoriesResponse>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Attachment, GetAllCategoriesResponse>> expression = e => new GetAllCategoriesResponse
            {
                Id = e.Id,
                FileName = e.FileName,
                FilePath = e.FilePath,
                FileType = e.FileType,
                FileSize = e.FileSize,
                KnowledgeBaseId = e.KnowledgeBaseId
            };
            var paginatedList = await _repository.Attachments
                .Select(expression)
                .ToPaginatedListAsync(request.PageNumber, request.PageSize);
            return paginatedList;
        }
    }
}