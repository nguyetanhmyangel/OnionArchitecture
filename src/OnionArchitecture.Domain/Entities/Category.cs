using System.ComponentModel.DataAnnotations.Schema;
using OnionArchitecture.Domain.Abstractions;

namespace OnionArchitecture.Domain.Entities
{
    [Table("Categories")]
    public class Category : AuditableBaseEntity<int>
    {
        public string Name { get; set; }
        public string SeoAlias { get; set; }
        public string SeoDescription { get; set; }
        public int SortOrder { get; set; }
        public int? ParentId { get; set; }
        public int? NumberOfTickets { get; set; }
    }
}