using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using OnionArchitecture.Application.Interfaces.Repositories;
using OnionArchitecture.Infrastructure.Share.Results;

namespace OnionArchitecture.Application.Features.MySpaces.Queries.GetById
{
    public class GetMySpaceByIdQuery : IRequest<Result<GetMySpaceByIdResponse>>
    {
        public int Id { get; set; }

        public class GetMySpaceByIdQueryHandler : IRequestHandler<GetMySpaceByIdQuery, Result<GetMySpaceByIdResponse>>
        {
            private readonly IMySpaceRepository _mySpaceRepository;
            private readonly IMapper _mapper;

            public GetMySpaceByIdQueryHandler(IMySpaceRepository mySpaceRepository, IMapper mapper)
            {
                _mySpaceRepository = mySpaceRepository;
                _mapper = mapper;
            }

            public async Task<Result<GetMySpaceByIdResponse>> Handle(GetMySpaceByIdQuery query, CancellationToken cancellationToken)
            {
                var mySpace = await _mySpaceRepository.GetByIdAsync(query.Id);
                var mappedMySpace = _mapper.Map<GetMySpaceByIdResponse>(mySpace);
                return await Result<GetMySpaceByIdResponse>.SuccessAsync(mappedMySpace);
            }
        }
    }
}