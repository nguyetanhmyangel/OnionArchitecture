using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using OnionArchitecture.Application.Interfaces.Repositories;
using OnionArchitecture.Infrastructure.Share.Results;

namespace OnionArchitecture.Application.Features.AppCommandFunctions.Queries.Get
{
    public class GetAppCommandFunctionQuery : IRequest<Result<List<GetAppCommandFunctionResponse>>>
    {
        public GetAppCommandFunctionQuery()
        {
        }
    }

    public class GetEnjoinFunctionQueryHandler : IRequestHandler<GetAppCommandFunctionQuery, Result<List<GetAppCommandFunctionResponse>>>
    {
        private readonly IAppCommandFunctionRepository _appCommandFunctionRepository;
        private readonly IMapper _mapper;

        public GetEnjoinFunctionQueryHandler(IAppCommandFunctionRepository appCommandFunctionRepository, IMapper mapper)
        {
            _appCommandFunctionRepository = appCommandFunctionRepository;
            _mapper = mapper;
        }

        public async Task<Result<List<GetAppCommandFunctionResponse>>> Handle(GetAppCommandFunctionQuery request, CancellationToken cancellationToken)
        {
            var appCommandFunctionList = await _appCommandFunctionRepository.GetListAsync();
            var mappedAppCommandFunctions = _mapper.Map<List<GetAppCommandFunctionResponse>>(appCommandFunctionList);
            return await Result<List<GetAppCommandFunctionResponse>>.SuccessAsync(mappedAppCommandFunctions);
        }
    }
}