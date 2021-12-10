using System.ComponentModel.DataAnnotations;
using OnionArchitecture.Domain.Abstractions;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnionArchitecture.Domain.Entities
{
    // Like Command
    [Table("Enjoins")]
    public class Enjoin : AuditableBaseEntity<int>
    {
        [MaxLength(50)]
        [Required]
        public string Name { get; set; }
    }
}