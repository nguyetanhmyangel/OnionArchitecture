using OnionArchitecture.Domain.Abstractions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnionArchitecture.Domain.Entities
{
    [Table("Attachments")]
    public class Attachment : AuditableBaseEntity<int>
    {
        [Required]
        [MaxLength(200)]
        public string FileName { get; set; }

        [Required]
        [MaxLength(200)]
        public string FilePath { get; set; }

        [Required]
        [MaxLength(4)]
        [Column(TypeName = "varchar(4)")]
        public string FileType { get; set; }

        [Required]
        public long FileSize { get; set; }

        public int? MySpaceId { get; set; }

        public int? CommentId { get; set; }

        public virtual MySpace MySpace { get; set; }

        public virtual Comment Comment { get; set; }
    }
}