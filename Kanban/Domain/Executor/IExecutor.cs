using System;
using Infrastructure;

namespace Domain
{
    public interface IExecutor : IEntity<Guid>
    {
        string Name { get; set; }
        string TelegramUsername { get; set; }
    }
}