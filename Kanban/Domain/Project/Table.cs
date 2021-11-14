using System.Collections.Generic;
using System.Linq;

namespace Domain
{
    public class Table
    {
        private readonly List<ITask> _tasks = new();
        private readonly Dictionary<IExecutor, AccessRights> _users = new();
        private readonly List<State> _states = new();
        public IReadOnlyCollection<State> States => _states.ToArray();
        public IReadOnlyCollection<ITask> Tasks => _tasks.ToArray();
        public IEnumerable<IExecutor> Team => _users.Keys; // по дефолту есть права на редактирование
        public IEnumerable<IExecutor> Readers => FilterExecutors(AccessRights.Read);
        public IEnumerable<IExecutor> Editors => FilterExecutors(AccessRights.Edit);
        public IEnumerable<IExecutor> Admins => FilterExecutors(AccessRights.Admin);

        public void AddTask(ITask task) => _tasks.Add(task);

        public void AddExecutor(IExecutor executor, AccessRights accessRights = AccessRights.Read) =>
            _users.Add(executor, accessRights);

        public void AddState(State state) => _states.Add(state);

        private IEnumerable<IExecutor> FilterExecutors(AccessRights accessRights) =>
            _users
                .Where(p => p.Value == accessRights)
                .Select(p => p.Key);
    }
}