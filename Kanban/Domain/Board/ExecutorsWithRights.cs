using System;
using Infrastructure;

namespace Domain
{
    public class ExecutorsWithRights : IEntity<Guid>
    {
        public ExecutorsWithRights(Guid executorId, AccessRights rights) =>
            (Id, ExecutorId, Rights) = (Guid.NewGuid(), executorId, rights);

        // ReSharper disable once UnusedMember.Local
        private ExecutorsWithRights()
        {
        }

        public Guid Id { get; }
        public Guid ExecutorId { get; }
        public AccessRights Rights { get; }
    }
}