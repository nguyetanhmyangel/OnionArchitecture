using System.ComponentModel.DataAnnotations;
using OnionArchitecture.Domain.Abstractions;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnionArchitecture.Domain.Entities
{
    [Table("Functions")]
    public class Function : AuditableBaseEntity<int>
    {
        [MaxLength(500)]
        [Required]
        public string Name { get; set; }

        [MaxLength(200)]
        [Required]
        public string Url { get; set; }

        [Required]
        public int SortOrder { get; set; }

        public int ParentId { get; set; }

        [MaxLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string Icon { get; set; }
    }
}