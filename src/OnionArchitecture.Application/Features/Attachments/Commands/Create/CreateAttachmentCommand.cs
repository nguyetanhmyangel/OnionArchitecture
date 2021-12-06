using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using OnionArchitecture.Application.Interfaces.Repositories;
using OnionArchitecture.Domain.Entities;
using OnionArchitecture.Infrastructure.Share.Results;

namespace OnionArchitecture.Application.Features.Attachments.Commands.Create
{
    public partial class CreateCategoryCommand : IRequest<Result<int>>
    {
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string FileType { get; set; }
        public long FileSize { get; set; }
        public int KnowledgeBaseId { get; set; }
    }

    public class CreateAttachmentCommandHandler : IRequestHandler<CreateCategoryCommand, Result<int>>
    {
        private readonly IAttachmentRepository _attachmentRepository;
        private readonly IMapper _mapper;

        private IUnitOfWork _unitOfWork { get; set; }

        public CreateAttachmentCommandHandler(IAttachmentRepository attachmentRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _attachmentRepository = attachmentRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<int>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var attachment = _mapper.Map<Attachment>(request);
            await _attachmentRepository.InsertAsync(attachment);
            await _unitOfWork.Commit(cancellationToken);
            return await Result<int>.SuccessAsync(attachment.Id);
        }
    }
}