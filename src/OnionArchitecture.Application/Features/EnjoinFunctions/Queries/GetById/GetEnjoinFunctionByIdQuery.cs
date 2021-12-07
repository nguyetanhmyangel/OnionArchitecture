using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using OnionArchitecture.Application.Interfaces.Repositories;
using OnionArchitecture.Infrastructure.Share.Results;

namespace OnionArchitecture.Application.Features.EnjoinFunctions.Queries.GetById
{
    public class GetEnjoinFunctionByIdQuery : IRequest<Result<GetEnjoinFunctionByIdResponse>>
    {
        public int Id { get; set; }

        public class GetProductByIdQueryHandler : IRequestHandler<GetEnjoinFunctionByIdQuery, Result<GetEnjoinFunctionByIdResponse>>
        {
            private readonly IEnjoinFunctionRepository _enjoinFunctionRepository;
            private readonly IMapper _mapper;

            public GetProductByIdQueryHandler(IEnjoinFunctionRepository enjoinFunctionRepository, IMapper mapper)
            {
                _enjoinFunctionRepository = enjoinFunctionRepository;
                _mapper = mapper;
            }

            public async Task<Result<GetEnjoinFunctionByIdResponse>> Handle(GetEnjoinFunctionByIdQuery query, CancellationToken cancellationToken)
            {
                var enjoinFunction = await _enjoinFunctionRepository.GetByIdAsync(query.Id);
                var mappedEnjoinFunction = _mapper.Map<GetEnjoinFunctionByIdResponse>(enjoinFunction);
                return await Result<GetEnjoinFunctionByIdResponse>.SuccessAsync(mappedEnjoinFunction);
            }
        }
    }
}