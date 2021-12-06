using Infrastructure;

namespace Domain
{
    public class Chat : IEntity<int>
    {
        public int Id { get; }
        public App App { get; }
        public string ProjectId { get; }

        public Chat(int id, App app, string projectId) => (Id, App, ProjectId) = (id, app, projectId);
    }
}