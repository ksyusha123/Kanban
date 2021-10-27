using System;

namespace Domain
{
    public interface IExecutor
    {
        string Name { get; set; }
        Guid Id { get; set; }
    }
}