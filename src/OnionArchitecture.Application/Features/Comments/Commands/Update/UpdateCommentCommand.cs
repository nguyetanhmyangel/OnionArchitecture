using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OnionArchitecture.Application.Interfaces.Repositories;
using OnionArchitecture.Infrastructure.Share.Results;

namespace OnionArchitecture.Application.Features.Comments.Commands.Update
{
    public class UpdateCommentCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int KnowledgeBaseId { get; set; }

        public class UpdateCommentCommandHandler : IRequestHandler<UpdateCommentCommand, Result<int>>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly ICommentRepository _commentRepository;

            public UpdateCommentCommandHandler(ICommentRepository commentRepository, IUnitOfWork unitOfWork)
            {
                _commentRepository = commentRepository;
                _unitOfWork = unitOfWork;
            }

            public async Task<Result<int>> Handle(UpdateCommentCommand command, CancellationToken cancellationToken)
            {
                var comment = await _commentRepository.GetByIdAsync(command.Id);

                if (comment == null)
                {
                    return await Result<int>.FailAsync($"Comment Not Found.");
                }

                comment.Content = command.Content ?? comment.Content;
                comment.KnowledgeBaseId = (command.KnowledgeBaseId == 0) ? comment.KnowledgeBaseId : command.KnowledgeBaseId;
                await _commentRepository.UpdateAsync(comment);
                await _unitOfWork.Commit(cancellationToken);
                return await Result<int>.SuccessAsync(comment.Id);
            }
        }
    }
}