using System.ComponentModel.DataAnnotations;
using OnionArchitecture.Domain.Abstractions;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnionArchitecture.Domain.Entities
{
    [Table("Comments")]
    public class Comment : AuditableBaseEntity<int>
    {
        [MaxLength(500)]
        [Required]
        public string Content { get; set; }

        [Required]
        public int MySpaceId { get; set; }

        public virtual MySpace MySpace { get; set; }
    }
}