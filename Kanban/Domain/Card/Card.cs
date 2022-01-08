using System;
using System.Collections.Generic;
using Infrastructure;

namespace Domain
{
    public class Card : IEntity<string>
    {
        private readonly List<Comment> _comments = new();

        // ReSharper disable once UnusedMember.Local
        private Card()
        {
        }

        public Card(string name, string description, Executor executor, string columnId, 
            IDateTimeProvider dateTimeProvider, string id="")
        {
            Id = id == "" ? Guid.NewGuid().ToString() : id;
            (Name, Description, Executor, ColumnId, CreationTime) =
                (name, description, executor, columnId, dateTimeProvider.GetCurrent());
        }

        public string Id { get; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public Executor? Executor { get; set; }
        public string ColumnId { get; set; }
        public IEnumerable<Comment> Comments => _comments;
        public DateTime CreationTime { get; }

        public void AddComment(Comment comment) => _comments.Add(comment);
    }
}