using System;

namespace OnionArchitecture.Domain.Abstractions
{
    public interface IAuditableBaseEntity
    {
        DateTime CreatedOn { get; set; }
        string CreatedBy { get; set; }
        DateTime? LastModifiedOn { get; set; }
        string LastModifiedBy { get; set; }
    }
}