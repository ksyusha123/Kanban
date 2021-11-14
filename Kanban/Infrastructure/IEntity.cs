using System;

namespace Infrastructure
{
    public interface IEntity : IEquatable<IEntity>
    {
        Guid Id { get; }

        bool IEquatable<IEntity>.Equals(IEntity other) => other is { } && Id == other.Id;
    }
}