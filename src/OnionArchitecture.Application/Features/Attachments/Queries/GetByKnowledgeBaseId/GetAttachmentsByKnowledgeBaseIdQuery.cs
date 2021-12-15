using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using OnionArchitecture.Application.Interfaces.Repositories;
using OnionArchitecture.Infrastructure.Share.Results;

namespace OnionArchitecture.Application.Features.Attachments.Queries.GetByKnowledgeBaseId
{
    public class GetAttachmentByKnowledgeBaseIdQuery : IRequest<Result<GetAttachmentByKnowledgeBaseIdResponse>>
    {
        public int KnowledgeBaseId { get; set; }

        public class GetAttachmentByKnowledgeBaseIdQueryHandler : IRequestHandler<GetAttachmentByKnowledgeBaseIdQuery, Result<GetAttachmentByKnowledgeBaseIdResponse>>
        {
            private readonly IAttachmentRepository _attachmentRepository;
            private readonly IMapper _mapper;

            public GetAttachmentByKnowledgeBaseIdQueryHandler(IAttachmentRepository attachmentRepository, IMapper mapper)
            {
                _attachmentRepository = attachmentRepository;
                _mapper = mapper;
            }

            public async Task<Result<GetAttachmentByKnowledgeBaseIdResponse>> Handle(GetAttachmentByKnowledgeBaseIdQuery query, CancellationToken cancellationToken)
            {
                var attachments = await _attachmentRepository.GetByIdAsync(query.KnowledgeBaseId);
                var mappedAttachments = _mapper.Map<GetAttachmentByKnowledgeBaseIdResponse>(attachments);
                return await Result<GetAttachmentByKnowledgeBaseIdResponse>.SuccessAsync(mappedAttachments);
            }
        }
    }
}