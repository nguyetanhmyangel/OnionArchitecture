using System.ComponentModel.DataAnnotations;
using OnionArchitecture.Domain.Abstractions;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnionArchitecture.Domain.Entities
{
    [Table("Labels")]
    public class Label : AuditableBaseEntity<int>
    {
        [MaxLength(50)]
        public string Name { get; set; }
    }
}