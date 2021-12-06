using System;
using System.Collections.Generic;
using FluentSpecification.Composite;
using FluentSpecification.Conclusion;
using FluentSpecification.Embedded;
using Infrastructure;

namespace Domain
{
    public class State : IEntity<Guid>
    {
        private string _name = string.Empty;

        // ReSharper disable once UnusedMember.Local
        private State()
        {
        }

        public State(string name, IReadOnlyCollection<State> prevStates, IReadOnlyCollection<State> nextStates) =>
            (Id, Name, PrevStates, NextStates) = (Guid.NewGuid(), name, prevStates, nextStates);

        public Guid Id { get; }

        public string Name
        {
            get => _name;
            set
            {
                _name = value;

                Specs
                    .For<State>()
                    .Member(t => t.Name, new StringNotEmptySpec()
                        .And(new StringMaxLengthSpec(100))
                        .And(new StringNotContinuousSpacesSpec())
                        .And(new StringNotEdgeSpaceSpec()))
                    .ThrowIfNotSatisfied(this);
            }
        }

        public IReadOnlyCollection<State> PrevStates { get; } = null!;
        public IReadOnlyCollection<State> NextStates { get; } = null!;
    }
}