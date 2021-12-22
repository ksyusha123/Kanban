using System;
using System.Collections.Generic;
using FluentSpecification.Composite;
using FluentSpecification.Conclusion;
using FluentSpecification.Embedded;
using Infrastructure;

namespace Domain
{
    public class Column : IEntity<Guid>
    {
        private string _name = string.Empty;

        // ReSharper disable once UnusedMember.Local
        private Column()
        {
        }

        public Column(string name, IReadOnlyCollection<Column> prevStates, IReadOnlyCollection<Column> nextStates) =>
            (Id, Name, PrevStates, NextStates) = (Guid.NewGuid(), name, prevStates, nextStates);

        public Guid Id { get; }

        public string Name
        {
            get => _name;
            set
            {
                _name = value;

                Specs
                    .For<Column>()
                    .Member(t => t.Name, new StringNotEmptySpec()
                        .And(new StringMaxLengthSpec(100))
                        .And(new StringNotContinuousSpacesSpec())
                        .And(new StringNotEdgeSpaceSpec()))
                    .ThrowIfNotSatisfied(this);
            }
        }

        public IReadOnlyCollection<Column> PrevStates { get; } = null!;
        public IReadOnlyCollection<Column> NextStates { get; } = null!;
    }
}