using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using OnionArchitecture.Application.Interfaces.Repositories;
using OnionArchitecture.Infrastructure.Share.Results;

namespace OnionArchitecture.Application.Features.LabelKnowledgeBases.Queries.GetById
{
    public class GetLabelKnowledgeBaseByIdQuery : IRequest<Result<GetLabelKnowledgeBaseByIdResponse>>
    {
        public int Id { get; set; }

        public class GetLabelKnowledgeBaseByIdQueryHandler : IRequestHandler<GetLabelKnowledgeBaseByIdQuery, Result<GetLabelKnowledgeBaseByIdResponse>>
        {
            private readonly ILabelKnowledgeBaseRepository _knowledgeBaseRepository;
            private readonly IMapper _mapper;

            public GetLabelKnowledgeBaseByIdQueryHandler(ILabelKnowledgeBaseRepository knowledgeBaseRepository, IMapper mapper)
            {
                _knowledgeBaseRepository = knowledgeBaseRepository;
                _mapper = mapper;
            }

            public async Task<Result<GetLabelKnowledgeBaseByIdResponse>> Handle(GetLabelKnowledgeBaseByIdQuery query, CancellationToken cancellationToken)
            {
                var labelKnowledgeBase = await _knowledgeBaseRepository.GetByIdAsync(query.Id);
                var mappedLabelKnowledgeBases = _mapper.Map<GetLabelKnowledgeBaseByIdResponse>(labelKnowledgeBase);
                return await Result<GetLabelKnowledgeBaseByIdResponse>.SuccessAsync(mappedLabelKnowledgeBases);
            }
        }
    }
}