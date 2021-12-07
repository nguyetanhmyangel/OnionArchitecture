using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using OnionArchitecture.Application.Interfaces.Repositories;
using OnionArchitecture.Infrastructure.Share.Results;

namespace OnionArchitecture.Application.Features.Enjoins.Queries.Get
{
    public class GetEnjoinQuery : IRequest<Result<List<GetEnjoinResponse>>>
    {
        public GetEnjoinQuery()
        {
        }
    }

    public class GetEnjoinQueryHandler : IRequestHandler<GetEnjoinQuery, Result<List<GetEnjoinResponse>>>
    {
        private readonly IEnjoinRepository _enjoinRepository;
        private readonly IMapper _mapper;

        public GetEnjoinQueryHandler(IEnjoinRepository enjoinRepository, IMapper mapper)
        {
            _enjoinRepository = enjoinRepository;
            _mapper = mapper;
        }

        public async Task<Result<List<GetEnjoinResponse>>> Handle(GetEnjoinQuery request, CancellationToken cancellationToken)
        {
            var enjointList = await _enjoinRepository.GetListAsync();
            var mappedEnjoins = _mapper.Map<List<GetEnjoinResponse>>(enjointList);
            return await Result<List<GetEnjoinResponse>>.SuccessAsync(mappedEnjoins);
        }
    }
}