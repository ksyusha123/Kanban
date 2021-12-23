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
        // ReSharper disable once UnusedMember.Local
        private Column()
        {
        }

        public Column(string name, int orderNumber) => (Id, Name, OrderNumber) = (Guid.NewGuid(), name, orderNumber);

        public Guid Id { get; }

        public string Name { get; set; } = null!;

        public int OrderNumber { get; }
    }
}