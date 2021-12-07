using OnionArchitecture.Domain.Entities;

namespace OnionArchitecture.Application.Features.Attachments.Queries.GetById
{
    public class GetAttachmentByIdResponse
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string FileType { get; set; }
        public long FileSize { get; set; }
        public string Type { get; set; }
        public int? MyBaseId { get; set; }
        public int? CommentId { get; set; }
    }
}