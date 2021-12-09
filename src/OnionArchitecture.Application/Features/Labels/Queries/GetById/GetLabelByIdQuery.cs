using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using OnionArchitecture.Application.Interfaces.Repositories;
using OnionArchitecture.Infrastructure.Share.Results;

namespace OnionArchitecture.Application.Features.Labels.Queries.GetById
{
    public class GetLabelByIdQuery : IRequest<Result<GetLabelByIdResponse>>
    {
        public int Id { get; set; }

        public class GetLabelByIdQueryHandler : IRequestHandler<GetLabelByIdQuery, Result<GetLabelByIdResponse>>
        {
            private readonly ILabelRepository _labelRepository;
            private readonly IMapper _mapper;

            public GetLabelByIdQueryHandler(ILabelRepository labelRepository, IMapper mapper)
            {
                _labelRepository = labelRepository;
                _mapper = mapper;
            }

            public async Task<Result<GetLabelByIdResponse>> Handle(GetLabelByIdQuery query, CancellationToken cancellationToken)
            {
                var label = await _labelRepository.GetByIdAsync(query.Id);
                var mappedLabel = _mapper.Map<GetLabelByIdResponse>(label);
                return await Result<GetLabelByIdResponse>.SuccessAsync(mappedLabel);
            }
        }
    }
}