using OnionArchitecture.Domain.Abstractions;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnionArchitecture.Domain.Entities
{
    [Table("Privileges")]
    public class Privilege : AuditableBaseEntity<int>
    {
        //public Privilege(string functionId, string roleId, string commandId)
        //{
        //    FunctionId = functionId;
        //    RoleId = roleId;
        //    CommandId = commandId;
        //}

        public int FunctionId { get; set; }
        public int RoleId { get; set; }
        public int EnjoinId { get; set; }
        public virtual Function Function { get; set; }
        public virtual Enjoin Enjoin { get; set; }
    }
}