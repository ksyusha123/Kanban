using System;

namespace Domain
{
    public class Executor : IExecutor
    {
        public Executor(Guid id, string name) => (Id, Name) = (id, name);

        public Guid Id { get; }
        public string Name { get; set; }
    }
}