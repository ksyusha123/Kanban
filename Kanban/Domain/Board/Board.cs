using System;
using System.Collections.Generic;
using System.Linq;
using Infrastructure;

namespace Domain
{
    public class Board : IEntity<string>
    {
        private readonly List<Card> _cards = new();
        private readonly Dictionary<Guid, AccessRights> _team = new();
        private readonly List<Column> _columns = new();

        public Board(string name, List<Column> columns, string id="")
        {
            Id = id == "" ? Guid.NewGuid().ToString() : id;
            Name = name;
            _columns = columns;
        }

        // ReSharper disable once UnusedMember.Local
        private Board()
        {
        }

        public string Id { get; }
        public string Name { get; } = null!;

        public Column StartColumn => _columns.Single(c => c.OrderNumber == 0);
        public IReadOnlyCollection<Column> Columns => _columns;
        public IReadOnlyCollection<Card> Cards => _cards;
        public IEnumerable<Guid> Team => _team.Keys;
        public IEnumerable<Guid> Readers => FilterExecutors(AccessRights.Read);
        public IEnumerable<Guid> Commentators => FilterExecutors(AccessRights.Comment);
        public IEnumerable<Guid> Editors => FilterExecutors(AccessRights.Edit);
        public IEnumerable<Guid> Admins => FilterExecutors(AccessRights.Admin);

        private IEnumerable<ExecutorsWithRights> ExecutorsWithRights =>
            _team.Select(i => new ExecutorsWithRights(i.Key, i.Value));

        public void AddCard(Card card) => _cards.Add(card);

        public void RemoveCard(Card card) => _cards.Remove(card);

        public void AddExecutor(Executor executor, AccessRights accessRights = AccessRights.Read) =>
            _team.Add(executor.Id, accessRights);

        public void ChangeExecutorRights(Executor executor, AccessRights accessRights) =>
            _team[executor.Id] = accessRights;

        private IEnumerable<Guid> FilterExecutors(AccessRights accessRights) =>
            _team
                .Where(p => p.Value == accessRights)
                .Select(p => p.Key);
    }
}