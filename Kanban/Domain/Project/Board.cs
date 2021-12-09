using System;
using System.Collections.Generic;
using System.Linq;
using Infrastructure;

namespace Domain
{
    public class Board : IEntity<Guid>
    {
        private readonly List<Card> _tasks = null!;
        private readonly Dictionary<Guid, AccessRights> _team = new();
        private readonly List<State> _states = null!;

        public Board(string name, List<Card> tasks, Dictionary<Guid, AccessRights> team, List<State> states) =>
            (Id, Name, _tasks, _team, _states) = (Guid.NewGuid(), name, tasks, team, states);

        private Board(Guid id, string name, List<Card> tasks, IEnumerable<ExecutorsWithRights> executors,
            List<State> states)
        {
            (Id, Name, _tasks, _states) = (id, name, tasks, states);

            foreach (var i in executors) _team[i.ExecutorId] = i.Rights;
        }

        // ReSharper disable once UnusedMember.Local
        private Board()
        {
        }

        public Guid Id { get; }
        public string Name { get; }

        public IReadOnlyCollection<State> States => _states.ToArray();
        public IReadOnlyCollection<Card> Tasks => _tasks.ToArray();
        public IEnumerable<Guid> Team => _team.Keys;
        public IEnumerable<Guid> Readers => FilterExecutors(AccessRights.Read);
        public IEnumerable<Guid> Commentators => FilterExecutors(AccessRights.Comment);
        public IEnumerable<Guid> Editors => FilterExecutors(AccessRights.Edit);
        public IEnumerable<Guid> Admins => FilterExecutors(AccessRights.Admin);

        private IEnumerable<ExecutorsWithRights> ExecutorsWithRights =>
            _team.Select(i => new ExecutorsWithRights(i.Key, i.Value));

        public void AddTask(Card card) => _tasks.Add(card);

        public void RemoveTask(Card card) => _tasks.Remove(card);

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