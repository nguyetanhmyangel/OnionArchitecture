using OnionArchitecture.Domain.Abstractions;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnionArchitecture.Domain.Entities
{
    [Table("Votes")]
    public class Vote : AuditableBaseEntity<int>
    {
        public int MyBaseId { get; set; }
        public virtual MyBase MyBase { get; set; }
    }
}