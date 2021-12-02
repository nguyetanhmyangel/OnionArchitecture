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
        public int CommandId { get; set; }
    }
}