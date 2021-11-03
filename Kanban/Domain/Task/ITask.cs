namespace Domain
{
    public interface ITask : IEntity
    {
        string Name { get; set; }
        IExecutor? Executor { get; set; }
        string Description { get; set; }
        IState State { get; set; }
    }
}