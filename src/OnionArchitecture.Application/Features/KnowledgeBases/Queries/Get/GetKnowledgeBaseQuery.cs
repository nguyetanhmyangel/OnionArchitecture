using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using OnionArchitecture.Application.Interfaces.Repositories;
using OnionArchitecture.Infrastructure.Share.Results;

namespace OnionArchitecture.Application.Features.KnowledgeBases.Queries.Get
{
    public class GetKnowledgeBaseQuery : IRequest<Result<List<GetKnowledgeBaseResponse>>>
    {
        public GetKnowledgeBaseQuery()
        {
        }
    }

    public class GetKnowledgeBaseQueryHandler : IRequestHandler<GetKnowledgeBaseQuery, Result<List<GetKnowledgeBaseResponse>>>
    {
        private readonly IKnowledgeBaseRepository _knowledgeBaseRepository;
        private readonly IMapper _mapper;

        public GetKnowledgeBaseQueryHandler(IKnowledgeBaseRepository knowledgeBaseRepository, IMapper mapper)
        {
            _knowledgeBaseRepository = knowledgeBaseRepository;
            _mapper = mapper;
        }

        public async Task<Result<List<GetKnowledgeBaseResponse>>> Handle(GetKnowledgeBaseQuery request, CancellationToken cancellationToken)
        {
            var knowledgeBaseList = await _knowledgeBaseRepository.GetListAsync();
            var mappedKnowledgeBases = _mapper.Map<List<GetKnowledgeBaseResponse>>(knowledgeBaseList);
            return await Result<List<GetKnowledgeBaseResponse>>.SuccessAsync(mappedKnowledgeBases);
        }
    }
}