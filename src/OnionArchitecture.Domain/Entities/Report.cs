using OnionArchitecture.Domain.Abstractions;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnionArchitecture.Domain.Entities
{
    [Table("Reports")]
    public class Report :AuditableBaseEntity<int>
    {
        public int MyBaseId { get; set; }
        public string Content { get; set; }
        public bool IsProcessed { get; set; }
        public virtual MyBase MyBase { get; set; }
    }
}