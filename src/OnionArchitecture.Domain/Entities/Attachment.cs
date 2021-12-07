using OnionArchitecture.Domain.Abstractions;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnionArchitecture.Domain.Entities
{
    [Table("Attachments")]
    public class Attachment : AuditableBaseEntity<int>
    {
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string FileType { get; set; }
        public long FileSize { get; set; }
        public string Type { get; set; }
        public int? MyBaseId { get; set; }
        public int? CommentId { get; set; }
        public virtual MySpace MySpace { get; set; }
        public virtual Comment Comment { get; set; }
    }
}