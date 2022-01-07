using Infrastructure;

namespace Domain
{
    public class Chat : IEntity<long>
    {
        // ReSharper disable once UnusedMember.Local
        private Chat()
        {
        }

        public Chat(long id, App app, string projectId) => (Id, App, BoardId) = (id, app, projectId);

        public long Id { get; }
        public App App { get; }
        public string BoardId { get; } = null!;
    }
}