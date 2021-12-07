using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using OnionArchitecture.Application.Interfaces.Repositories;
using OnionArchitecture.Infrastructure.Share.Results;

namespace OnionArchitecture.Application.Features.Enjoins.Queries.GetById
{
    public class GetEnjoinByIdQuery : IRequest<Result<GetEnjoinByIdResponse>>
    {
        public int Id { get; set; }

        public class GetProductByIdQueryHandler : IRequestHandler<GetEnjoinByIdQuery, Result<GetEnjoinByIdResponse>>
        {
            private readonly IEnjoinRepository _enjoinRepository;
            private readonly IMapper _mapper;

            public GetProductByIdQueryHandler(IEnjoinRepository enjoinRepository, IMapper mapper)
            {
                _enjoinRepository = enjoinRepository;
                _mapper = mapper;
            }

            public async Task<Result<GetEnjoinByIdResponse>> Handle(GetEnjoinByIdQuery query, CancellationToken cancellationToken)
            {
                var enjoin = await _enjoinRepository.GetByIdAsync(query.Id);
                var mappedEnjoin = _mapper.Map<GetEnjoinByIdResponse>(enjoin);
                return await Result<GetEnjoinByIdResponse>.SuccessAsync(mappedEnjoin);
            }
        }
    }
}