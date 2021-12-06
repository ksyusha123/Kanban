using Infrastructure;

namespace Domain
{
    public interface IProject<T> : IEntity<T>
    {
        App App { get; }
    }
}