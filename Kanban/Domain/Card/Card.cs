﻿using System;
using System.Collections.Generic;
using FluentSpecification.Composite;
using FluentSpecification.Conclusion;
using FluentSpecification.Embedded;
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

        public Card(string name, string description, Executor executor, Column column,
            IDateTimeProvider dateTimeProvider) =>
            (Id, Name, Description, Executor, Column, CreationTime) =
            (Guid.NewGuid(), name, description, executor, column, dateTimeProvider.GetCurrent());

        public Guid Id { get; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public Executor? Executor { get; set; }
        public Column Column { get; set; } = null!;
        public IEnumerable<Comment> Comments => _comments;
        public DateTime CreationTime { get; }

        public void AddComment(Comment comment) => _comments.Add(comment);
    }
}