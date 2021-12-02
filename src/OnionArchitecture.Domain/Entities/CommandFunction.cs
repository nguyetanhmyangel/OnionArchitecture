using OnionArchitecture.Domain.Abstractions;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnionArchitecture.Domain.Entities
{
    [Table("CommandFunctions")]
    public class CommandFunction : AuditableBaseEntity<int>
    {
        public int CommandId { get; set; }
        public int FunctionId { get; set; }
    }
}