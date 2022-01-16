using Infrastructure;

namespace Domain
{
    public class Chat : IEntity
    {
        // ReSharper disable once UnusedMember.Local
        private Chat()
        {
        }

        public Chat(string id, App app, string boardId) => (Id, App, BoardId) = (id, app, boardId);

        public string Id { get; }
        public App App { get; }
        public string BoardId { get; } = null!;
    }
}