using System;
using Infrastructure;

namespace Domain
{
    public class Comment : IEntity
    {
        // ReSharper disable once UnusedMember.Local
        private Comment()
        {
        }

        public Comment(Executor author, string message, IDateTimeProvider dateTimeProvider) =>
            (Id, Author, Message, CreationTime) = (Guid.NewGuid().ToString(), author, message, dateTimeProvider.GetCurrent());

        public string Id { get; }
        public Executor Author { get; } = null!;
        public string Message { get; } = null!;
        public DateTime CreationTime { get; }
        public override string ToString() => $"{Author.Id} [{CreationTime}]: {Message}";
    }
}