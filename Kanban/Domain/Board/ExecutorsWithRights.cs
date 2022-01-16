using System;
using Infrastructure;

namespace Domain
{
    public class ExecutorsWithRights : IEntity
    {
        public ExecutorsWithRights(string executorId, AccessRights rights) =>
            (Id, ExecutorId, Rights) = (Guid.NewGuid().ToString(), executorId, rights);

        // ReSharper disable once UnusedMember.Local
        private ExecutorsWithRights()
        {
        }

        public string Id { get; }
        public string ExecutorId { get; }
        public AccessRights Rights { get; }
    }
}