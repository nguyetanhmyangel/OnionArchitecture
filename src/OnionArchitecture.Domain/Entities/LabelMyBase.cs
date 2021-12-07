using OnionArchitecture.Domain.Abstractions;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnionArchitecture.Domain.Entities
{
    [Table("LabelMyBases")]
    public class LabelMyBase : AuditableBaseEntity<int>
    {
        public int MyBaseId { get; set; }
        public int LabelId { get; set; }
        public virtual MyBase MyBase { get; set; }
        public virtual Label Label { get; set; }
    }
}