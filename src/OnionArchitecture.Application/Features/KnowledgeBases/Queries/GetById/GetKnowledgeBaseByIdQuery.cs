using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using OnionArchitecture.Application.Interfaces.Repositories;
using OnionArchitecture.Infrastructure.Share.Results;

namespace OnionArchitecture.Application.Features.KnowledgeBases.Queries.GetById
{
    public class GetKnowledgeBaseByIdQuery : IRequest<Result<GetKnowledgeBaseByIdResponse>>
    {
        public int Id { get; set; }

        public class GetKnowledgeBaseByIdQueryHandler : IRequestHandler<GetKnowledgeBaseByIdQuery, Result<GetKnowledgeBaseByIdResponse>>
        {
            private readonly IKnowledgeBaseRepository _knowledgeBaseRepository;
            private readonly IMapper _mapper;

            public GetKnowledgeBaseByIdQueryHandler(IKnowledgeBaseRepository knowledgeBaseRepository, IMapper mapper)
            {
                _knowledgeBaseRepository = knowledgeBaseRepository;
                _mapper = mapper;
            }

            public async Task<Result<GetKnowledgeBaseByIdResponse>> Handle(GetKnowledgeBaseByIdQuery query, CancellationToken cancellationToken)
            {
                var knowledgeBase = await _knowledgeBaseRepository.GetByIdAsync(query.Id);
                var mappedKnowledgeBase = _mapper.Map<GetKnowledgeBaseByIdResponse>(knowledgeBase);
                return await Result<GetKnowledgeBaseByIdResponse>.SuccessAsync(mappedKnowledgeBase);
            }
        }
    }
}