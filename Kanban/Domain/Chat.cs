using Infrastructure;

namespace Domain
{
    public class Chat : IEntity<int>
    {
        public int Id { get; }
        public App App { get; }
        public int ProjectId { get; }

        public Chat(int id, App app, int boardId) => (Id, App, ProjectId) = (id, app, boardId);
    }
}