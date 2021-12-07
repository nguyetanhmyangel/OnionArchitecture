using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using OnionArchitecture.Application.Interfaces.Repositories;
using OnionArchitecture.Infrastructure.Share.Results;

namespace OnionArchitecture.Application.Features.LabelMySpaces.Queries.Get
{
    public class GetLabelMySpaceQuery : IRequest<Result<List<GetLabelMySpaceResponse>>>
    {
        public GetLabelMySpaceQuery()
        {
        }
    }

    public class GetLabelMySpaceQueryHandler : IRequestHandler<GetLabelMySpaceQuery, Result<List<GetLabelMySpaceResponse>>>
    {
        private readonly ILabelMySpaceRepository _labelMySpaceRepository;
        private readonly IMapper _mapper;

        public GetLabelMySpaceQueryHandler(ILabelMySpaceRepository labelMySpaceRepository, IMapper mapper)
        {
            _labelMySpaceRepository = labelMySpaceRepository;
            _mapper = mapper;
        }

        public async Task<Result<List<GetLabelMySpaceResponse>>> Handle(GetLabelMySpaceQuery request, CancellationToken cancellationToken)
        {
            var labelMySpaceList = await _labelMySpaceRepository.GetListAsync();
            var mappedLabelMySpaces = _mapper.Map<List<GetLabelMySpaceResponse>>(labelMySpaceList);
            return await Result<List<GetLabelMySpaceResponse>>.SuccessAsync(mappedLabelMySpaces);
        }
    }
}