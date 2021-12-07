using OnionArchitecture.Domain.Abstractions;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnionArchitecture.Domain.Entities
{
    [Table("LabelMySpaces")]
    public class LabelMySpace : AuditableBaseEntity<int>
    {
        public int MySpaceId { get; set; }
        public int LabelId { get; set; }
        public virtual MySpace MySpace { get; set; }
        public virtual Label Label { get; set; }
    }
}