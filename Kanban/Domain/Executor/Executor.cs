using System;

namespace Domain
{
    public class Executor : IExecutor
    {
        public Executor(string name, string telegramUsername) =>
            (Id, Name, TelegramUsername) = (Guid.NewGuid(), name, telegramUsername);

        public Guid Id { get; }
        public string Name { get; set; }
        public string TelegramUsername { get; set; }
    }
}