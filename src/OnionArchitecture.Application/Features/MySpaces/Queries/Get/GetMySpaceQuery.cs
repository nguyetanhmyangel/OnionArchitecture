using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using OnionArchitecture.Application.Interfaces.Repositories;
using OnionArchitecture.Infrastructure.Share.Results;

namespace OnionArchitecture.Application.Features.MySpaces.Queries.Get
{
    public class GetMySpaceQuery : IRequest<Result<List<GetMySpaceResponse>>>
    {
        public GetMySpaceQuery()
        {
        }
    }

    public class GetMySpaceQueryHandler : IRequestHandler<GetMySpaceQuery, Result<List<GetMySpaceResponse>>>
    {
        private readonly IMySpaceRepository _mySpaceRepository;
        private readonly IMapper _mapper;

        public GetMySpaceQueryHandler(IMySpaceRepository mySpaceRepository, IMapper mapper)
        {
            _mySpaceRepository = mySpaceRepository;
            _mapper = mapper;
        }

        public async Task<Result<List<GetMySpaceResponse>>> Handle(GetMySpaceQuery request, CancellationToken cancellationToken)
        {
            var mySpaceList = await _mySpaceRepository.GetListAsync();
            var mappedMySpaces = _mapper.Map<List<GetMySpaceResponse>>(mySpaceList);
            return await Result<List<GetMySpaceResponse>>.SuccessAsync(mappedMySpaces);
        }
    }
}