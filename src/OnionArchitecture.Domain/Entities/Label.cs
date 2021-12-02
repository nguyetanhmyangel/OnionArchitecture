using OnionArchitecture.Domain.Abstractions;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnionArchitecture.Domain.Entities
{
    [Table("Labels")]
    public class Label : AuditableBaseEntity<int>
    {
        public string Name { get; set; }
    }
}