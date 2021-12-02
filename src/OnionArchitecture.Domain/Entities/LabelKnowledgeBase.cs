using OnionArchitecture.Domain.Abstractions;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnionArchitecture.Domain.Entities
{
    [Table("LabelKnowledgeBases")]
    public class LabelKnowledgeBase : AuditableBaseEntity<int>
    {
        public int KnowledgeBaseId { get; set; }

        public int LabelId { get; set; }
    }
}