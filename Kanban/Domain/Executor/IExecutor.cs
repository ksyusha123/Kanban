using Infrastucture;

namespace Domain
{
    public interface IExecutor : IEntity
    {
        string Name { get; set; }
    }
}