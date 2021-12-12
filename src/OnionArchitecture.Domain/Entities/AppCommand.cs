using System.ComponentModel.DataAnnotations;
using OnionArchitecture.Domain.Abstractions;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnionArchitecture.Domain.Entities
{
    // Like Command
    [Table("AppCommands")]
    public class AppCommand : AuditableBaseEntity<int>
    {
        [MaxLength(50)]
        [Required]
        public string Name { get; set; }
    }
}