using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using OnionArchitecture.Infrastructure.Share.Results;

namespace OnionArchitecture.Application.Features.Enjoins.Queries.Get
{
    public class GetEnjoinQuery : IRequest<Result<List<GetEnjoinResponse>>>
    {
        public GetEnjoinQuery()
        {
        }
    }

    public class GetAllProductsCachedQueryHandler : IRequestHandler<GetEnjoinQuery, Result<List<GetEnjoinResponse>>>
    {
        private readonly IProductCacheRepository _productCache;
        private readonly IMapper _mapper;

        public GetAllProductsCachedQueryHandler(IProductCacheRepository productCache, IMapper mapper)
        {
            _productCache = productCache;
            _mapper = mapper;
        }

        public async Task<Result<List<GetEnjoinResponse>>> Handle(GetEnjoinQuery request, CancellationToken cancellationToken)
        {
            var productList = await _productCache.GetCachedListAsync();
            var mappedProducts = _mapper.Map<List<GetEnjoinResponse>>(productList);
            return Result<List<GetEnjoinResponse>>.Success(mappedProducts);
        }
    }
}