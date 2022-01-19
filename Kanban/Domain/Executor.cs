using System;
using Infrastructure;

namespace Domain
{
    public class Executor : IEntity
    {
        public Executor(string name, string id) : this(id, name, Guid.NewGuid().ToString())
        {
        }

        public Executor(string id, string name, string appUsername) =>
            (Id, Name, AppUsername) = (id, name, appUsername);

        public string Id { get; } // telegramusername
        public string Name { get; set; }
        public string AppUsername { get; set; }
    }
}