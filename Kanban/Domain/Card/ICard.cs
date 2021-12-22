using System;
using Infrastructure;

namespace Domain
{
    public interface ICard : IEntity<Guid>
    {
        string Name { get; set; }
        Executor? Executor { get; set; }
        string Description { get; set; }
        Column Column { get; set; }
        DateTime CreationTime { get; }
    }
}