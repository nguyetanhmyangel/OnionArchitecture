using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using OnionArchitecture.Application.Interfaces.Repositories;
using OnionArchitecture.Infrastructure.Share.Results;

namespace OnionArchitecture.Application.Features.EnjoinFunctions.Queries.Get
{
    public class GetEnjoinFunctionQuery : IRequest<Result<List<GetEnjoinFunctionResponse>>>
    {
        public GetEnjoinFunctionQuery()
        {
        }
    }

    public class GetEnjoinFunctionQueryHandler : IRequestHandler<GetEnjoinFunctionQuery, Result<List<GetEnjoinFunctionResponse>>>
    {
        private readonly IEnjoinFunctionRepository _enjoinFunctionRepository;
        private readonly IMapper _mapper;

        public GetEnjoinFunctionQueryHandler(IEnjoinFunctionRepository productCache, IMapper mapper)
        {
            _enjoinFunctionRepository = productCache;
            _mapper = mapper;
        }

        public async Task<Result<List<GetEnjoinFunctionResponse>>> Handle(GetEnjoinFunctionQuery request, CancellationToken cancellationToken)
        {
            var enjoinFunctionList = await _enjoinFunctionRepository.GetListAsync();
            var mappedProducts = _mapper.Map<List<GetEnjoinFunctionResponse>>(enjoinFunctionList);
            return await Result<List<GetEnjoinFunctionResponse>>.SuccessAsync(mappedProducts);
        }
    }
}