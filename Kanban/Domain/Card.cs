using System;
using System.Collections.Generic;
using Infrastructure;

namespace Domain
{
    public class Card : IEntity
    {
        private readonly List<Comment> _comments = new();

        // ReSharper disable once UnusedMember.Local
        private Card()
        {
        }

        public Card(string id, string name, string description, string columnId,
            IDateTimeProvider dateTimeProvider) =>
            (Id, Name, Description, ColumnId, CreationTime) =
            (id, name, description, columnId, dateTimeProvider.GetCurrent());

        public Card(string name, string description, string columnId,
            IDateTimeProvider dateTimeProvider) 
            : this(Guid.NewGuid().ToString(), name, description, columnId, dateTimeProvider)
        {
        }

        public string Id { get; } = null!;

        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public Executor? Executor { get; set; }
        public string ColumnId { get; set; } = null!;
        public IEnumerable<Comment> Comments => _comments;
        public DateTime CreationTime { get; }

        public void AddComment(Comment comment) => _comments.Add(comment);
    }
}