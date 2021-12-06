using System;

namespace Infrastructure
{
    public interface IEntity<T> : IEquatable<IEntity<T>>
    {
        T Id { get; }

        bool IEquatable<IEntity<T>>.Equals(IEntity<T> other) => other is { } && Id.Equals(other.Id);
    }
}