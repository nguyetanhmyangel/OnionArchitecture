using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using OnionArchitecture.Application.Interfaces.Repositories;
using OnionArchitecture.Infrastructure.Share.Results;

namespace OnionArchitecture.Application.Features.Attachments.Queries.Get
{
    public class GetAttachmentQuery : IRequest<Result<List<GetAttachmentResponse>>>
    {
        public GetAttachmentQuery()
        {
        }
    }

    public class GetAllAttachmentQueryHandler : IRequestHandler<GetAttachmentQuery, Result<List<GetAttachmentResponse>>>
    {
        private readonly IAttachmentRepository _attachmentRepository;
        private readonly IMapper _mapper;

        public GetAllAttachmentQueryHandler(IAttachmentRepository attachmentRepository, IMapper mapper)
        {
            _attachmentRepository = attachmentRepository;
            _mapper = mapper;
        }

        public async Task<Result<List<GetAttachmentResponse>>> Handle(GetAttachmentQuery request, CancellationToken cancellationToken)
        {
            var attachmentList = await _attachmentRepository.GetListAsync();
            var mappedAttachments = _mapper.Map<List<GetAttachmentResponse>>(attachmentList);
            return await Result<List<GetAttachmentResponse>>.SuccessAsync(mappedAttachments);
        }
    }
}