using System;

namespace Domain
{
    public class Executor : IExecutor
    {
        public Executor(string name, string telegramUsername) =>
            (Id, Name, TelegramUsername) = (Guid.NewGuid().ToString(), name, telegramUsername);

        public string Id { get; }
        public string Name { get; set; }
        public string TelegramUsername { get; set; }
    }
}