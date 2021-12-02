using OnionArchitecture.Domain.Abstractions;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnionArchitecture.Domain.Entities
{
    [Table("Comments")]
    public class Comment : AuditableBaseEntity<int>
    {
        public string Content { get; set; }
        public int KnowledgeBaseId { get; set; }
        public string UserId { get; set; }
        public int? ReplyId { get; set; }
    }
}