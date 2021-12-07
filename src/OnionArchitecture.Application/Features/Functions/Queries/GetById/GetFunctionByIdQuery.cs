using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using OnionArchitecture.Application.Interfaces.Repositories;
using OnionArchitecture.Infrastructure.Share.Results;

namespace OnionArchitecture.Application.Features.Functions.Queries.GetById
{
    public class GetFunctionByIdQuery : IRequest<Result<GetFunctionByIdResponse>>
    {
        public int Id { get; set; }

        public class GetProductByIdQueryHandler : IRequestHandler<GetFunctionByIdQuery, Result<GetFunctionByIdResponse>>
        {
            private readonly IFunctionRepository _functionRepository;
            private readonly IMapper _mapper;

            public GetProductByIdQueryHandler(IFunctionRepository functionRepository, IMapper mapper)
            {
                _functionRepository = functionRepository;
                _mapper = mapper;
            }

            public async Task<Result<GetFunctionByIdResponse>> Handle(GetFunctionByIdQuery query, CancellationToken cancellationToken)
            {
                var function = await _functionRepository.GetByIdAsync(query.Id);
                var mappedFunctions = _mapper.Map<GetFunctionByIdResponse>(function);
                return await Result<GetFunctionByIdResponse>.SuccessAsync(mappedFunctions);
            }
        }
    }
}