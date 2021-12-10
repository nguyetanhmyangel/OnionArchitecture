using OnionArchitecture.Domain.Abstractions;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnionArchitecture.Domain.Entities
{
    [Table("EnjoinFunctions")]
    public class EnjoinFunction : AuditableBaseEntity<int>
    {
        public int EnjoinId { get; set; }

        public int FunctionId { get; set; }

        public virtual Enjoin Enjoin { get; set; }

        public virtual Function Function { get; set; }
    }
}