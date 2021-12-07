using OnionArchitecture.Domain.Abstractions;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnionArchitecture.Domain.Entities
{
    [Table("Votes")]
    public class Vote : AuditableBaseEntity<int>
    {
        public int MySpaceId { get; set; }
        public virtual MySpace MySpace { get; set; }
    }
}