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

namespace OnionArchitecture.Application.Features.Attachments.Queries.GetPage
{
    public class GetPageAttachmentQuery : IRequest<PaginatedResult<GetPageAttachmentResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public GetPageAttachmentQuery(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }

    public class GetPageAttachmentQueryHandler : IRequestHandler<GetPageAttachmentQuery, PaginatedResult<GetPageAttachmentResponse>>
    {
        private readonly IAttachmentRepository _repository;

        public GetPageAttachmentQueryHandler(IAttachmentRepository repository)
        {
            _repository = repository;
        }

        public async Task<PaginatedResult<GetPageAttachmentResponse>> Handle(GetPageAttachmentQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Attachment, GetPageAttachmentResponse>> expression = e => new GetPageAttachmentResponse
            {
                Id = e.Id,
                FileName = e.FileName,
                FilePath = e.FilePath,
                FileType = e.FileType,
                FileSize = e.FileSize,
                KnowledgeBaseId = e.KnowledgeBaseId,
                CommentId = e.CommentId
            };
            var paginatedList = await _repository.Attachments
                .Select(expression)
                .ToPaginatedListAsync(request.PageNumber, request.PageSize);
            return paginatedList;
        }
    }
}