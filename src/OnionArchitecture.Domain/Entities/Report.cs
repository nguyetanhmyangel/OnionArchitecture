using System.ComponentModel.DataAnnotations;
using OnionArchitecture.Domain.Abstractions;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnionArchitecture.Domain.Entities
{
    [Table("Reports")]
    public class Report :AuditableBaseEntity<int>
    {
        public int KnowledgeBaseId { get; set; }

        [MaxLength(500)]
        public string Content { get; set; }

        public bool IsProcessed { get; set; }

        public virtual KnowledgeBase KnowledgeBase { get; set; }
    }
}