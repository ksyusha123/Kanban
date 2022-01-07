using System;
using System.Collections.Generic;
using Infrastructure;

namespace Domain
{
    public class Card : ICard
    {
        private readonly List<Comment> _comments = new();

        // ReSharper disable once UnusedMember.Local
        private Card()
        {
        }

        public Card(string name, string description, Executor executor, Guid columnId, 
            IDateTimeProvider dateTimeProvider) =>
            (Id, Name, Description, Executor, ColumnId, CreationTime) =
            (Guid.NewGuid(), name, description, executor, columnId, dateTimeProvider.GetCurrent());

        public Guid Id { get; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public Executor? Executor { get; set; }
        public Guid ColumnId { get; set; }
        public IEnumerable<Comment> Comments => _comments;
        public DateTime CreationTime { get; }

        public void AddComment(Comment comment) => _comments.Add(comment);
    }
}