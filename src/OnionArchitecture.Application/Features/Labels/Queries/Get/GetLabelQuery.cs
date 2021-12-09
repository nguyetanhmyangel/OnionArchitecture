using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using OnionArchitecture.Application.Interfaces.Repositories;
using OnionArchitecture.Infrastructure.Share.Results;

namespace OnionArchitecture.Application.Features.Labels.Queries.Get
{
    public class GetLabelQuery : IRequest<Result<List<GetLabelResponse>>>
    {
        public GetLabelQuery()
        {
        }
    }

    public class GetLabelQueryHandler : IRequestHandler<GetLabelQuery, Result<List<GetLabelResponse>>>
    {
        private readonly ILabelRepository _labelRepository;
        private readonly IMapper _mapper;

        public GetLabelQueryHandler(ILabelRepository labelRepository, IMapper mapper)
        {
            _labelRepository = labelRepository;
            _mapper = mapper;
        }

        public async Task<Result<List<GetLabelResponse>>> Handle(GetLabelQuery request, CancellationToken cancellationToken)
        {
            var labelList = await _labelRepository.GetListAsync();
            var mappedLabels = _mapper.Map<List<GetLabelResponse>>(labelList);
            return await Result<List<GetLabelResponse>>.SuccessAsync(mappedLabels);
        }
    }
}