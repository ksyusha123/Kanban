using System;
using System.Collections.Generic;
using System.Linq;
using FluentSpecification.Composite;
using FluentSpecification.Conclusion;
using FluentSpecification.Embedded;
using Infrastucture;

namespace Domain
{
    public class Project : IEntity
    {
        private string _name = string.Empty;
        private string _description = string.Empty;
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
        public Project(Guid id, IList<Table> tables) => (Id, _tables) = (id, tables);

        public Guid Id { get; }
        private readonly IList<Table> _tables;

        public IReadOnlyCollection<Table> Tables => _tables.ToArray();

        public void AddTable(Table table)
        {
            _tables.Add(table);
        }
    }
}