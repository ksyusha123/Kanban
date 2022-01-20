using System;
using Infrastructure;

namespace Domain
{
    public class Executor : IEntity
    {
        public Executor(string id) : this(id, Guid.NewGuid().ToString())
        {
        }

        public Executor(string id, string appUsername) =>
            (Id, AppUsername) = (id, appUsername);

        public string Id { get; } // telegramusername
        public string AppUsername { get; set; }
    }
}