using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using OnionArchitecture.Application.Interfaces.Repositories;
using OnionArchitecture.Infrastructure.Share.Results;

namespace OnionArchitecture.Application.Features.Attachments.Queries.GetAll
{
    public class GetAllCategoriesQuery : IRequest<Result<List<GetAllCategoriesResponse>>>
    {
        public GetAllCategoriesQuery()
        {
        }
    }

    public class GetAllAttachmentsCachedQueryHandler : IRequestHandler<GetAllCategoriesQuery, Result<List<GetAllCategoriesResponse>>>
    {
        private readonly IAttachmentRepository _attachmentCache;
        private readonly IMapper _mapper;

        public GetAllAttachmentsCachedQueryHandler(IAttachmentRepository attachmentCache, IMapper mapper)
        {
            _attachmentCache = attachmentCache;
            _mapper = mapper;
        }

        public async Task<Result<List<GetAllCategoriesResponse>>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            var attachmentsList = await _attachmentCache.GetListAsync();
            var mappedAttachments = _mapper.Map<List<GetAllCategoriesResponse>>(attachmentsList);
            return await Result<List<GetAllCategoriesResponse>>.SuccessAsync(mappedAttachments);
        }
    }
}