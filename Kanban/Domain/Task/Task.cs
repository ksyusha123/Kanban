using System;
using System.Collections.Generic;
using FluentSpecification.Composite;
using FluentSpecification.Conclusion;
using FluentSpecification.Embedded;
using Infrastucture;

namespace Domain
{
    public class Task : ITask
    {
        private string name = string.Empty;
        private string description = string.Empty;

        public List<Comment> Comments { get; set; }

        private Task(Guid id, string name, IExecutor? executor, string description, State state) =>
            (Id, this.name, Executor, this.description, State) = (id, name, executor, description, state);

        public Task(string name, IExecutor? executor, string description, State state,
            IDateTimeProvider dateTimeProvider) =>
            (Id, Name, Executor, Description, State, CreationTime) =
            (Guid.NewGuid(), name, executor, description, state, dateTimeProvider.GetCurrent());

        public Guid Id { get; }

        public string Name
        {
            get => name;
            set
            {
                name = value;

                Specs
                    .For<ITask>()
                    .Member(t => t.Name, new StringNotEmptySpec()
                        .And(new StringMaxLengthSpec(100))
                        .And(new StringNotContinuousSpacesSpec())
                        .And(new StringNotEdgeSpaceSpec()))
                    .ThrowIfNotSatisfied(this);
            }
        }

        public IExecutor? Executor { get; set; }

        public string Description
        {
            get => description;
            set
            {
                description = value;

                Specs
                    .For<ITask>()
                    .Member(t => t.Description, new StringMaxLengthSpec(250)
                        .And(new StringNotContinuousSpacesSpec())
                        .And(new StringNotEdgeSpaceSpec())
                        .And(new StringMatchSpec("\n").Not()))
                    .ThrowIfNotSatisfied(this);
            }
        }

        public State State { get; set; }
        public DateTime CreationTime { get; }
    }
}