using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using OnionArchitecture.Application.Interfaces.Repositories;
using OnionArchitecture.Infrastructure.Share.Results;

namespace OnionArchitecture.Application.Features.AppCommands.Queries.Get
{
    public class GetAppCommandQuery : IRequest<Result<List<GetAppCommandResponse>>>
    {
        public GetAppCommandQuery()
        {
        }
    }

    public class GetEnjoinQueryHandler : IRequestHandler<GetAppCommandQuery, Result<List<GetAppCommandResponse>>>
    {
        private readonly IAppCommandRepository _enjoinRepository;
        private readonly IMapper _mapper;

        public GetEnjoinQueryHandler(IAppCommandRepository enjoinRepository, IMapper mapper)
        {
            _enjoinRepository = enjoinRepository;
            _mapper = mapper;
        }

        public async Task<Result<List<GetAppCommandResponse>>> Handle(GetAppCommandQuery request, CancellationToken cancellationToken)
        {
            var appCommandList = await _enjoinRepository.GetListAsync();
            var mappedEnjoins = _mapper.Map<List<GetAppCommandResponse>>(appCommandList);
            return await Result<List<GetAppCommandResponse>>.SuccessAsync(mappedEnjoins);
        }
    }
}