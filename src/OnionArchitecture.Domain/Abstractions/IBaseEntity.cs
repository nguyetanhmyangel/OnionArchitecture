﻿namespace OnionArchitecture.Domain.Abstractions
{
    public interface IBaseEntity<T> //where T : struct
    {
        T Id { get; set; }
        //bool IsTransient();
    }
}