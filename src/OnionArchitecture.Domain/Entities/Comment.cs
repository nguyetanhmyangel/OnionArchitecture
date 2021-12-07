using OnionArchitecture.Domain.Abstractions;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnionArchitecture.Domain.Entities
{
    [Table("Comments")]
    public class Comment : AuditableBaseEntity<int>
    {
        public string Content { get; set; }
        public int MyBaseId { get; set; }
        public virtual MyBase MyBase { get; set; }
    }
}