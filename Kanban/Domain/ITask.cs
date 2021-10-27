using System;

namespace Domain
{
    public interface ITask
    {
        string Name { get; set; }
        Guid Id { get; set; }
        IExecutor Executor { get; set; }
        string Description { get; set; }
        IState State { get; set; }
    }
}