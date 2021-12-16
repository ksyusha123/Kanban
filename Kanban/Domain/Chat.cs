using Infrastructure;

namespace Domain
{
    public class Chat : IEntity<long>
    {
        public long Id { get; }
        public App App { get; }
        public string BoardId { get; }

        public Chat(long id, App app, string projectId) => (Id, App, BoardId) = (id, app, projectId);
    }
}