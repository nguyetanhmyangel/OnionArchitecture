using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using OnionArchitecture.Application.Interfaces.Repositories;
using OnionArchitecture.Infrastructure.Share.Results;

namespace OnionArchitecture.Application.Features.LabelKnowledgeBases.Queries.Get
{
    public class GetLabelKnowledgeBaseQuery : IRequest<Result<List<GetLabelKnowledgeBaseResponse>>>
    {
        public GetLabelKnowledgeBaseQuery()
        {
        }
    }

    public class GetLabelKnowledgeBaseQueryHandler : IRequestHandler<GetLabelKnowledgeBaseQuery, Result<List<GetLabelKnowledgeBaseResponse>>>
    {
        private readonly ILabelKnowledgeBaseRepository _labelKnowledgeBaseRepository;
        private readonly IMapper _mapper;

        public GetLabelKnowledgeBaseQueryHandler(ILabelKnowledgeBaseRepository labelKnowledgeBaseRepository, IMapper mapper)
        {
            _labelKnowledgeBaseRepository = labelKnowledgeBaseRepository;
            _mapper = mapper;
        }

        public async Task<Result<List<GetLabelKnowledgeBaseResponse>>> Handle(GetLabelKnowledgeBaseQuery request, CancellationToken cancellationToken)
        {
            var labelKnowledgeBaseList = await _labelKnowledgeBaseRepository.GetListAsync();
            var mappedLabelKnowledgeBases = _mapper.Map<List<GetLabelKnowledgeBaseResponse>>(labelKnowledgeBaseList);
            return await Result<List<GetLabelKnowledgeBaseResponse>>.SuccessAsync(mappedLabelKnowledgeBases);
        }
    }
}