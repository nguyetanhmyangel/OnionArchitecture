using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using OnionArchitecture.Application.Interfaces.Repositories;
using OnionArchitecture.Infrastructure.Share.Results;

namespace OnionArchitecture.Application.Features.Functions.Queries.Get
{
    public class GetFunctionQuery : IRequest<Result<List<GetFunctionResponse>>>
    {
        public GetFunctionQuery()
        {
        }
    }

    public class GetFunctionQueryHandler : IRequestHandler<GetFunctionQuery, Result<List<GetFunctionResponse>>>
    {
        private readonly IFunctionRepository _functionRepository;
        private readonly IMapper _mapper;

        public GetFunctionQueryHandler(IFunctionRepository functionRepository, IMapper mapper)
        {
            _functionRepository = functionRepository;
            _mapper = mapper;
        }

        public async Task<Result<List<GetFunctionResponse>>> Handle(GetFunctionQuery request, CancellationToken cancellationToken)
        {
            var functionRepositoryList = await _functionRepository.GetListAsync();
            var mappedFunctions = _mapper.Map<List<GetFunctionResponse>>(functionRepositoryList);
            return await Result<List<GetFunctionResponse>>.SuccessAsync(mappedFunctions);
        }
    }
}