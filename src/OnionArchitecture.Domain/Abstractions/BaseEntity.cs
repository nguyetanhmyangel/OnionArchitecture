
namespace OnionArchitecture.Domain.Abstractions
{
    public abstract class BaseEntity<T> : IBaseEntity<T>
    {
        public T Id { get; set; }

        ///// <summary>
        ///// True if domain entity has an identity
        ///// </summary>
        ///// <returns></returns>
        //public bool IsTransient()
        //{
        //    return Id.Equals(default(T));
        //}
    }
}