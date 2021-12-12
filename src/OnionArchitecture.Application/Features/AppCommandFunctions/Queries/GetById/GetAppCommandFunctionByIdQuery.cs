using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using OnionArchitecture.Application.Interfaces.Repositories;
using OnionArchitecture.Infrastructure.Share.Results;

namespace OnionArchitecture.Application.Features.AppCommandFunctions.Queries.GetById
{
    public class GetAppCommandFunctionByIdQuery : IRequest<Result<GetAppCommandFunctionByIdResponse>>
    {
        public int Id { get; set; }

        public class GetProductByIdQueryHandler : IRequestHandler<GetAppCommandFunctionByIdQuery, Result<GetAppCommandFunctionByIdResponse>>
        {
            private readonly IAppCommandFunctionRepository _appCommandFunctionRepository;
            private readonly IMapper _mapper;

            public GetProductByIdQueryHandler(IAppCommandFunctionRepository appCommandFunctionRepository, IMapper mapper)
            {
                _appCommandFunctionRepository = appCommandFunctionRepository;
                _mapper = mapper;
            }

            public async Task<Result<GetAppCommandFunctionByIdResponse>> Handle(GetAppCommandFunctionByIdQuery query, CancellationToken cancellationToken)
            {
                var appCommandFunction = await _appCommandFunctionRepository.GetByIdAsync(query.Id);
                var mappedAppCommandFunction = _mapper.Map<GetAppCommandFunctionByIdResponse>(appCommandFunction);
                return await Result<GetAppCommandFunctionByIdResponse>.SuccessAsync(mappedAppCommandFunction);
            }
        }
    }
}