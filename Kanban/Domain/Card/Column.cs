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

        public Column(string name, int orderNumber, string id="")
        {
            Id = id == "" ? Guid.NewGuid().ToString() : id;
            (Name, OrderNumber) = (name, orderNumber);
        }

        public string Id { get; }

        public string Name { get; set; } = null!;

        public int OrderNumber { get; }
    }
}