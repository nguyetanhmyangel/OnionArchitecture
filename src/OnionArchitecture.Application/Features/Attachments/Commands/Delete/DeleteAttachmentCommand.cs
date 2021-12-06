using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OnionArchitecture.Application.Interfaces.Repositories;
using OnionArchitecture.Infrastructure.Share.Results;

namespace OnionArchitecture.Application.Features.Attachments.Commands.Delete
{
    public class DeleteCategoryCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }

        public class DeleteAttachmentCommandHandler : IRequestHandler<DeleteCategoryCommand, Result<int>>
        {
            private readonly IAttachmentRepository _attachmentRepository;
            private readonly IUnitOfWork _unitOfWork;

            public DeleteAttachmentCommandHandler(IAttachmentRepository attachmentRepository, IUnitOfWork unitOfWork)
            {
                _attachmentRepository = attachmentRepository;
                _unitOfWork = unitOfWork;
            }

            public async Task<Result<int>> Handle(DeleteCategoryCommand command, CancellationToken cancellationToken)
            {
                var attachment = await _attachmentRepository.GetByIdAsync(command.Id);
                await _attachmentRepository.DeleteAsync(attachment);
                await _unitOfWork.Commit(cancellationToken);
                return await Result<int>.SuccessAsync(attachment.Id);
            }
        }
    }
}