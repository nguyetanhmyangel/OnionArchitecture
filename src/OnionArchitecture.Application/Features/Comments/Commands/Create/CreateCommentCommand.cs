using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using OnionArchitecture.Application.Interfaces.Repositories;
using OnionArchitecture.Domain.Entities;
using OnionArchitecture.Infrastructure.Share.Results;

namespace OnionArchitecture.Application.Features.Comments.Commands.Create
{
    public partial class CreateCommentCommand : IRequest<Result<int>>
    {
        public string Content { get; set; }
        public int MyBaseId { get; set; }
    }

    public class CreateProductCommandHandler : IRequestHandler<CreateCommentCommand, Result<int>>
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;

        private IUnitOfWork _unitOfWork { get; set; }

        public CreateProductCommandHandler(ICommentRepository commentRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _commentRepository = commentRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<int>> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
        {
            var comment = _mapper.Map<Comment>(request);
            await _commentRepository.InsertAsync(comment);
            await _unitOfWork.Commit(cancellationToken);
            return await Result<int>.SuccessAsync(comment.Id);
        }
    }
}