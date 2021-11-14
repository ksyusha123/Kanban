using System;
using Infrastucture;

namespace Domain
{
    public class Comment : IEntity
    {
        public Guid Id { get; }
        public IExecutor Author { get; }
        public string Message { get; }
        public DateTime CreationTime { get; }

        public Comment(Guid id, IExecutor author, string message, IDateTimeProvider dateTimeProvider)
        {
            Id = id;
            Author = author;
            Message = message;
            CreationTime = dateTimeProvider.GetCurrent();
        }
    }
}