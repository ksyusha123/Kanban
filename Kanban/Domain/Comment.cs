using System;
using Infrastructure;

namespace Domain
{
    public class Comment : IEntity<Guid>
    {
        // ReSharper disable once UnusedMember.Local
        private Comment()
        {
        }

        public Comment(Executor author, string message, IDateTimeProvider dateTimeProvider) =>
            (Id, Author, Message, CreationTime) = (Guid.NewGuid(), author, message, dateTimeProvider.GetCurrent());

        public Guid Id { get; }
        public Executor Author { get; } = null!;
        public string Message { get; } = null!;
        public DateTime CreationTime { get; }
    }
}