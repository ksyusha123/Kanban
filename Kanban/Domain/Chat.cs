using Infrastructure;

namespace Domain
{
    public class Chat : IEntity<long>
    {
        public long Id { get; }
        public App App { get; }
        public string ProjectId { get; }

        public Chat(long id, App app, string projectId) => (Id, App, ProjectId) = (id, app, projectId);
    }
}