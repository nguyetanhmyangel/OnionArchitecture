using OnionArchitecture.Domain.Entities;

namespace OnionArchitecture.Application.Features.Attachments.Queries.GetPage
{
    public class GetPageAttachmentResponse
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string FileType { get; set; }
        public long FileSize { get; set; }
        public int? KnowledgeBaseId { get; set; }
        public int? CommentId { get; set; }
        public string Type { get; set; }
    }
}