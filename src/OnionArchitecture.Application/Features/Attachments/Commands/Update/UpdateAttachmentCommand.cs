using MediatR;
using OnionArchitecture.Application.Interfaces.Repositories;
using OnionArchitecture.Infrastructure.Share.Results;
using System.Threading;
using System.Threading.Tasks;

namespace OnionArchitecture.Application.Features.Attachments.Commands.Update
{
    public class UpdateCategoryCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string FileType { get; set; }
        public long FileSize { get; set; }
        public int KnowledgeBaseId { get; set; }

        public class UpdateAttachmentCommandHandler : IRequestHandler<UpdateCategoryCommand, Result<int>>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IAttachmentRepository _attachmentRepository;

            public UpdateAttachmentCommandHandler(IAttachmentRepository attachmentRepository, IUnitOfWork unitOfWork)
            {
                _attachmentRepository = attachmentRepository;
                _unitOfWork = unitOfWork;
            }

            public async Task<Result<int>> Handle(UpdateCategoryCommand command, CancellationToken cancellationToken)
            {
                var attachment = await _attachmentRepository.GetByIdAsync(command.Id);

                if (attachment == null)
                {
                    return await Result<int>.FailAsync($"Attachment Not Found.");
                }
                else
                {
                    attachment.FileName = command.FileName ?? attachment.FileName;
                    attachment.FilePath = command.FilePath ?? attachment.FilePath;
                    attachment.FileSize = (command.FileSize == 0) ? attachment.FileSize : command.FileSize;
                    attachment.FileType = command.FileName ?? attachment.FileName;
                    attachment.KnowledgeBaseId = (command.KnowledgeBaseId == 0) ? attachment.KnowledgeBaseId : command.KnowledgeBaseId;
                    await _attachmentRepository.UpdateAsync(attachment);
                    await _unitOfWork.Commit(cancellationToken);
                    return await Result<int>.SuccessAsync(attachment.Id);
                }
            }
        }
    }
}