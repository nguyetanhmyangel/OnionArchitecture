using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using OnionArchitecture.Application.Interfaces.Repositories;
using OnionArchitecture.Infrastructure.Share.Results;

namespace OnionArchitecture.Application.Features.Attachments.Queries.GetById
{
    public class GetAttachmentByIdQuery : IRequest<Result<GetAttachmentByIdResponse>>
    {
        public int Id { get; set; }

        public class GetAttachmentByIdQueryHandler : IRequestHandler<GetAttachmentByIdQuery, Result<GetAttachmentByIdResponse>>
        {
            private readonly IAttachmentRepository _attachmentRepository;
            private readonly IMapper _mapper;

            public GetAttachmentByIdQueryHandler(IAttachmentRepository attachmentRepository, IMapper mapper)
            {
                _attachmentRepository = attachmentRepository;
                _mapper = mapper;
            }

            public async Task<Result<GetAttachmentByIdResponse>> Handle(GetAttachmentByIdQuery query, CancellationToken cancellationToken)
            {
                var attachments = await _attachmentRepository.GetByIdAsync(query.Id);
                var mappedAttachments = _mapper.Map<GetAttachmentByIdResponse>(attachments);
                return await Result<GetAttachmentByIdResponse>.SuccessAsync(mappedAttachments);
            }
        }
    }
}