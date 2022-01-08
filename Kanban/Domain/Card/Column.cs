using System;
using Infrastructure;

namespace Domain
{
    public class Column : IEntity<string>
    {
        // ReSharper disable once UnusedMember.Local
        private Column()
        {
        }
        
        public Column(string id, string name, int orderNumber) => (Id, Name, OrderNumber) = (id, name, orderNumber);

        public Column(string name, int orderNumber) : this(Guid.NewGuid().ToString(), name, orderNumber)
        {
        }

        public string Id { get; } = null!;

        public string Name { get; } = null!;

        public int OrderNumber { get; }
    }
}