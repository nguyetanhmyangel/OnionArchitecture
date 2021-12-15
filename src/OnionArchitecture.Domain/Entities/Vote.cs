using OnionArchitecture.Domain.Abstractions;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnionArchitecture.Domain.Entities
{
    [Table("Votes")]
    public class Vote : AuditableBaseEntity<int>
    {
        public int KnowledgeBaseId { get; set; }

        public virtual KnowledgeBase KnowledgeBase { get; set; }
    }
}