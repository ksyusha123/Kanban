using System;
using System.Collections.Generic;
using System.Linq;
using Infrastructure;

namespace Domain
{
    public class Board : IEntity
    {
        private readonly List<Card> _cards = new();
        private readonly Dictionary<string, AccessRights> _team = new();

        public Board(string name, List<Column> columns) : this(Guid.NewGuid().ToString(), name, columns)
        {
        }

        public Board(string id, string name, List<Column> columns) => (Id, Name, Columns) = (id, name, columns);

        // ReSharper disable once UnusedMember.Local
        private Board()
        {
        }

        public string Id { get; } = null!;
        public string Name { get; } = null!;

        public Column StartColumn => Columns.Single(c => c.OrderNumber == 0);
        public List<Column> Columns { get; set; } = null!;
        public IReadOnlyCollection<Card> Cards => _cards;

        public ILookup<Column, Card> CardsByColumns
        {
            get
            {
                var columnsDict = Columns.ToDictionary(c => c.Id);
                return Cards.Select(c => (Card: c, Column: columnsDict[c.ColumnId]))
                    .ToLookup(c => c.Column, c => c.Card);
            }
        }

        public IEnumerable<string> Team => _team.Keys;
        public IEnumerable<string> Readers => FilterExecutors(AccessRights.Read);
        public IEnumerable<string> Commentators => FilterExecutors(AccessRights.Comment);
        public IEnumerable<string> Editors => FilterExecutors(AccessRights.Edit);
        public IEnumerable<string> Admins => FilterExecutors(AccessRights.Admin);

        private IEnumerable<ExecutorsWithRights> ExecutorsWithRights =>
            _team.Select(i => new ExecutorsWithRights(i.Key, i.Value));

        public void AddCard(Card card) => _cards.Add(card);

        public void RemoveCard(Card card) => _cards.Remove(card);

        public void AddExecutor(Executor executor, AccessRights accessRights = AccessRights.Read) =>
            _team.Add(executor.Id, accessRights);

        public void ChangeExecutorRights(Executor executor, AccessRights accessRights) =>
            _team[executor.Id] = accessRights;

        private IEnumerable<string> FilterExecutors(AccessRights accessRights) =>
            _team
                .Where(p => p.Value == accessRights)
                .Select(p => p.Key);
    }
}