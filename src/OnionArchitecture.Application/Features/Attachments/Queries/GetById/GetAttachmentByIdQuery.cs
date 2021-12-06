using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using OnionArchitecture.Application.Interfaces.Repositories;
using OnionArchitecture.Infrastructure.Share.Results;

namespace OnionArchitecture.Application.Features.Attachments.Queries.GetById
{
    public class GetCategoryByIdQuery : IRequest<Result<GetAttachmentByIdResponse>>
    {
        public int Id { get; set; }

        public class GetAttachmentByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, Result<GetAttachmentByIdResponse>>
        {
            private readonly IAttachmentRepository _attachmentCache;
            private readonly IMapper _mapper;

            public GetAttachmentByIdQueryHandler(IAttachmentRepository attachmentCache, IMapper mapper)
            {
                _attachmentCache = attachmentCache;
                _mapper = mapper;
            }

            public async Task<Result<GetAttachmentByIdResponse>> Handle(GetCategoryByIdQuery query, CancellationToken cancellationToken)
            {
                var attachment = await _attachmentCache.GetByIdAsync(query.Id);
                var mappedAttachment = _mapper.Map<GetAttachmentByIdResponse>(attachment);
                return await Result<GetAttachmentByIdResponse>.SuccessAsync(mappedAttachment);
            }
        }
    }
}