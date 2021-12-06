using System;
using System.Collections.Generic;
using System.Linq;
using FluentSpecification.Composite;
using FluentSpecification.Conclusion;
using FluentSpecification.Embedded;
using Infrastructure;

namespace Domain
{
    public class Project : IProject<Guid>
    {
        private string _name = string.Empty;
        private string _description = string.Empty;
        private readonly List<Board> _tables = new();

        private Project()
        {
        }

        public Project(string name, string description, IEnumerable<Board> tables) =>
            (Id, Name, Description, _tables) = (Guid.NewGuid(), name, description, tables.ToList());

        public Guid Id { get; }

        public string Name
        {
            get => _name;
            set
            {
                _name = value;

                Specs
                    .For<Project>()
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
                    .For<Project>()
                    .Member(t => t.Description, new StringMaxLengthSpec(250)
                        .And(new StringNotContinuousSpacesSpec())
                        .And(new StringNotEdgeSpaceSpec())
                        .And(new StringMatchSpec("\n").Not()))
                    .ThrowIfNotSatisfied(this);
            }
        }

        public IEnumerable<Board> Tables => _tables.ToArray();

        public void AddTable(Board board) => _tables.Add(board);

        public void RemoveTable(Board board) => _tables.Remove(board);
        public App App => App.OwnKanban;
    }
}