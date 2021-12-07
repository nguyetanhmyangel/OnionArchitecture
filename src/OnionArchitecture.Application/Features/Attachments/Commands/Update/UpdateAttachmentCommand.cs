using MediatR;
using OnionArchitecture.Application.Interfaces.Repositories;
using OnionArchitecture.Infrastructure.Share.Results;
using System.Threading;
using System.Threading.Tasks;

namespace OnionArchitecture.Application.Features.Attachments.Commands.Update
{
    public class UpdateAttachmentCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string FileType { get; set; }
        public long FileSize { get; set; }
        public int? MyBaseId { get; set; }
        public int? CommentId { get; set; }
        public string Type { get; set; }

        public class UpdateAttachmentCommandHandler : IRequestHandler<UpdateAttachmentCommand, Result<int>>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IAttachmentRepository _attachmentRepository;

            public UpdateAttachmentCommandHandler(IAttachmentRepository attachmentRepository, IUnitOfWork unitOfWork)
            {
                _attachmentRepository = attachmentRepository;
                _unitOfWork = unitOfWork;
            }

            public async Task<Result<int>> Handle(UpdateAttachmentCommand command, CancellationToken cancellationToken)
            {
                var attachment = await _attachmentRepository.GetByIdAsync(command.Id);

                if (attachment == null)
                {
                    return await Result<int>.FailAsync($"Brand Not Found.");
                }

                attachment.FileName = command.FileName ?? attachment.FileName;
                attachment.FilePath = command.FilePath ?? attachment.FilePath;
                attachment.FileType = command.FileType ?? attachment.FileType;
                attachment.FileSize = command.FileSize != 0 ? command.FileSize : attachment.FileSize;
                attachment.MyBaseId = command.MyBaseId != 0 ? command.MyBaseId : attachment.MyBaseId;
                attachment.CommentId = command.CommentId != 0 ? command.CommentId : attachment.CommentId;
                await _attachmentRepository.UpdateAsync(attachment);
                await _unitOfWork.Commit(cancellationToken);
                return await Result<int>.SuccessAsync(attachment.Id);
            }
        }
    }
}