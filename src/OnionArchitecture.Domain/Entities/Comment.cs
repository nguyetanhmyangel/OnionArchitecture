using OnionArchitecture.Domain.Abstractions;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnionArchitecture.Domain.Entities
{
    [Table("Comments")]
    public class Comment : AuditableBaseEntity<int>
    {
        public string Content { get; set; }
        public int MySpaceId { get; set; }
        public virtual MySpace MySpace { get; set; }
    }
}