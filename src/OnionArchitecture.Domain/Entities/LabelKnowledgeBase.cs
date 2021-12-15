using OnionArchitecture.Domain.Abstractions;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnionArchitecture.Domain.Entities
{
    [Table("LabelKnowledgeBases")]
    public class LabelKnowledgeBase : AuditableBaseEntity<int>
    {
        public int KnowledgeBaseId { get; set; }

        public int LabelId { get; set; }

        public virtual KnowledgeBase KnowledgeBase { get; set; }

        public virtual Label Label { get; set; }
    }
}