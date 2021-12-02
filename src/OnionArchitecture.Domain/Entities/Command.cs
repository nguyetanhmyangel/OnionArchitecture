using OnionArchitecture.Domain.Abstractions;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnionArchitecture.Domain.Entities
{
    [Table("Commands")]
    public class Command : AuditableBaseEntity<int>
    {
        public string Name { get; set; }
    }
}