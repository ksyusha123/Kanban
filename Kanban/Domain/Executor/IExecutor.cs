using System;
using Infrastructure;

namespace Domain
{
    public interface IExecutor : IEntity
    {
        string Name { get; set; }
        string TelegramUsername { get; set; }
    }
}