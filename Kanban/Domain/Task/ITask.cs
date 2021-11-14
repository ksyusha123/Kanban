using System;
using Infrastucture;

namespace Domain
{
    public interface ITask : IEntity
    {
        string Name { get; set; }
        IExecutor? Executor { get; set; }
        string Description { get; set; }
        State State { get; set; }
        DateTime CreationTime { get; }
    }
}