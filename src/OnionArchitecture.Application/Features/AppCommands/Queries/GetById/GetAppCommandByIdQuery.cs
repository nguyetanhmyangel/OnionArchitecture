using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using OnionArchitecture.Application.Interfaces.Repositories;
using OnionArchitecture.Infrastructure.Share.Results;

namespace OnionArchitecture.Application.Features.Enjoins.Queries.GetById
{
    public class GetAppCommandByIdQuery : IRequest<Result<GetAppCommandByIdResponse>>
    {
        public int Id { get; set; }

        public class GetProductByIdQueryHandler : IRequestHandler<GetAppCommandByIdQuery, Result<GetAppCommandByIdResponse>>
        {
            private readonly IAppCommandRepository _enjoinRepository;
            private readonly IMapper _mapper;

            public GetProductByIdQueryHandler(IAppCommandRepository enjoinRepository, IMapper mapper)
            {
                _enjoinRepository = enjoinRepository;
                _mapper = mapper;
            }

            public async Task<Result<GetAppCommandByIdResponse>> Handle(GetAppCommandByIdQuery query, CancellationToken cancellationToken)
            {
                var enjoin = await _enjoinRepository.GetByIdAsync(query.Id);
                var mappedEnjoin = _mapper.Map<GetAppCommandByIdResponse>(enjoin);
                return await Result<GetAppCommandByIdResponse>.SuccessAsync(mappedEnjoin);
            }
        }
    }
}