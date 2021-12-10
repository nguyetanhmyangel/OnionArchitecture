﻿using System;
using System.ComponentModel.DataAnnotations;

namespace OnionArchitecture.Domain.Abstractions
{
    public abstract class AuditableBaseEntity<T> : IAuditableBaseEntity, IBaseEntity<T> //where T : struct
    {
        public T Id { get; set; }

        [MaxLength(50)]
        public string CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        [MaxLength(50)]
        public string LastModifiedBy { get; set; }

        public DateTime? LastModifiedOn { get; set; }

        //public bool IsTransient()
        //{
        //    throw new NotImplementedException();
        //}
    }
}