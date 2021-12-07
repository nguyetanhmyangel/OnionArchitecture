using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OnionArchitecture.Application.Interfaces.Repositories;
using OnionArchitecture.Infrastructure.Share.Results;

namespace OnionArchitecture.Application.Features.Comments.Commands.Delete
{
    public class DeleteCommentCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }

        public class DeleteCommentCommandHandler : IRequestHandler<DeleteCommentCommand, Result<int>>
        {
            private readonly ICommentRepository _commentRepository;
            private readonly IUnitOfWork _unitOfWork;

            public DeleteCommentCommandHandler(ICommentRepository commentRepository, IUnitOfWork unitOfWork)
            {
                _commentRepository = commentRepository;
                _unitOfWork = unitOfWork;
            }

            public async Task<Result<int>> Handle(DeleteCommentCommand command, CancellationToken cancellationToken)
            {
                var comment = await _commentRepository.GetByIdAsync(command.Id);
                await _commentRepository.DeleteAsync(comment);
                await _unitOfWork.Commit(cancellationToken);
                return await Result<int>.SuccessAsync(comment.Id);
            }
        }
    }
}