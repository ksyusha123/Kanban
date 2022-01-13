using Infrastructure;

namespace Domain
{
    public class Chat : IEntity<long>
    {
        // ReSharper disable once UnusedMember.Local
        private Chat()
        {
        }

        public Chat(long id, App app, string boardId) => (Id, App, BoardId) = (id, app, boardId);

        public long Id { get; }
        public App App { get; }
        public string BoardId { get; } = null!;
    }
}