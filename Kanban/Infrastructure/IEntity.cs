using System;

namespace Infrastructure
{
    public interface IEntity : IEquatable<IEntity>
    {
        string Id { get; }

        bool IEquatable<IEntity>.Equals(IEntity other) => other is { } && Id.Equals(other.Id);
    }
}