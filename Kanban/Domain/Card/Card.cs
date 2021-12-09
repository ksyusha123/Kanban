using System;
using System.Collections.Generic;
using FluentSpecification.Composite;
using FluentSpecification.Conclusion;
using FluentSpecification.Embedded;
using Infrastructure;

namespace Domain
{
    public class Card : ICard
    {
        private string _name = string.Empty;
        private string _description = string.Empty;
        private readonly List<Comment> _comments = new();

        // ReSharper disable once UnusedMember.Local
        private Card()
        {
        }

        public Card(string name, string description, Executor executor, State state,
            IDateTimeProvider dateTimeProvider) =>
            (Id, Name, Description, Executor, State, CreationTime) =
            (Guid.NewGuid(), name, description, executor, state, dateTimeProvider.GetCurrent());

        public Guid Id { get; }

        public string Name
        {
            get => _name;
            set
            {
                _name = value;

                Specs
                    .For<ICard>()
                    .Member(t => t.Name, new StringNotEmptySpec()
                        .And(new StringMaxLengthSpec(100))
                        .And(new StringNotContinuousSpacesSpec())
                        .And(new StringNotEdgeSpaceSpec()))
                    .ThrowIfNotSatisfied(this);
            }
        }

        public string Description
        {
            get => _description;
            set
            {
                _description = value;

                Specs
                    .For<ICard>()
                    .Member(t => t.Description, new StringMaxLengthSpec(250)
                        .And(new StringNotContinuousSpacesSpec())
                        .And(new StringNotEdgeSpaceSpec())
                        .And(new StringMatchSpec("\n").Not()))
                    .ThrowIfNotSatisfied(this);
            }
        }

        public Executor? Executor { get; set; }
        public State State { get; set; } = null!;
        public IEnumerable<Comment> Comments => _comments;
        public DateTime CreationTime { get; }

        public void AddComment(Comment comment) => _comments.Add(comment);
    }
}