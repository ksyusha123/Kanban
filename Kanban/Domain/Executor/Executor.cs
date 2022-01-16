using System;

namespace Domain
{
    public class Executor : IExecutor
    {
        public Executor(string name, string telegramUsername) : this(Guid.NewGuid().ToString(), name, telegramUsername)
        {
        }

        public Executor(string id, string name, string telegramUsername) =>
            (Id, Name, TelegramUsername) = (id, name, telegramUsername);

        public string Id { get; }
        public string Name { get; set; }
        public string TelegramUsername { get; set; }
    }
}