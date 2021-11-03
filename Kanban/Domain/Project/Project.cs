using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain
{
    public class Project : IEntity
    {
        public Project(Guid id, IList<Table> tables) => (Id, this.tables) = (id, tables);

        public Guid Id { get; }
        private readonly IList<Table> tables;

        public IReadOnlyCollection<Table> Tables => tables.ToArray();
    }
}