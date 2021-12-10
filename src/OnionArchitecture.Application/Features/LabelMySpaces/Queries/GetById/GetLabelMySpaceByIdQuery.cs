using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using OnionArchitecture.Application.Interfaces.Repositories;
using OnionArchitecture.Infrastructure.Share.Results;

namespace OnionArchitecture.Application.Features.LabelMySpaces.Queries.GetById
{
    public class GetLabelMySpaceByIdQuery : IRequest<Result<GetLabelMySpaceByIdResponse>>
    {
        public int Id { get; set; }

        public class GetLabelMySpaceByIdQueryHandler : IRequestHandler<GetLabelMySpaceByIdQuery, Result<GetLabelMySpaceByIdResponse>>
        {
            private readonly ILabelMySpaceRepository _productCache;
            private readonly IMapper _mapper;

            public GetLabelMySpaceByIdQueryHandler(ILabelMySpaceRepository productCache, IMapper mapper)
            {
                _productCache = productCache;
                _mapper = mapper;
            }

            public async Task<Result<GetLabelMySpaceByIdResponse>> Handle(GetLabelMySpaceByIdQuery query, CancellationToken cancellationToken)
            {
                var labelMySpace = await _productCache.GetByIdAsync(query.Id);
                var mappedLabelMySpaces = _mapper.Map<GetLabelMySpaceByIdResponse>(labelMySpace);
                return await Result<GetLabelMySpaceByIdResponse>.SuccessAsync(mappedLabelMySpaces);
            }
        }
    }
}