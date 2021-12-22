﻿using System;
using System.Collections.Generic;
using System.Linq;
using Infrastructure;

namespace Domain
{
    public class Board : IEntity<Guid>
    {
        private readonly List<Card> _cards = new();
        private readonly Dictionary<Guid, AccessRights> _team = new();
        private readonly List<Column> _states = new();

        public Board(string name, List<Card> cards, Dictionary<Guid, AccessRights> team, List<Column> states) =>
            (Id, Name, _cards, _team, _states) = (Guid.NewGuid(), name, cards, team, states);

        public Board(string name) => (Id, Name) = (Guid.NewGuid(), name);

        private Board(Guid id, string name, List<Card> cards, List<Column> states)
        {
            (Id, Name, _cards, _states) = (id, name, cards, states);

            // foreach (var i in executors) _team[i.ExecutorId] = i.Rights;
        }

        // ReSharper disable once UnusedMember.Local
        private Board()
        {
        }

        public Guid Id { get; }
        public string Name { get; }

        public IReadOnlyCollection<Column> States => _states;
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