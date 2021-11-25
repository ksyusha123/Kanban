using System;
using Infrastucture;

namespace Domain
{
    public class Comment
    {
        public IExecutor Author { get; }
        public string Message { get; }
        public DateTime CreationTime { get; }

        public Comment(IExecutor author, string message, IDateTimeProvider dateTimeProvider)
        {
            Author = author;
            Message = message;
            CreationTime = dateTimeProvider.GetCurrent();
        }
    }
}