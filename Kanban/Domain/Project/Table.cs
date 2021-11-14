using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain
{
    public class Table
    {
        private IEnumerable<ITask> _tasks = new List<ITask>();
        private IEnumerable<IExecutor> _teamMembers = new List<IExecutor>();
        private Dictionary<IExecutor, AccessRights> _users = new Dictionary<IExecutor, AccessRights>();
        private IEnumerable<State> _states = new List<State>();
        public IReadOnlyCollection<State> States => _states.ToArray();
        public IReadOnlyCollection<ITask> Tasks => _tasks.ToArray();
        public IReadOnlyCollection<IExecutor> Team => _teamMembers.ToArray(); // по дефолту есть права на редактирование

        public void AddTask(ITask task)
        {
            _tasks = _tasks.Append(task);
        }

        public void AddTeamMember(IExecutor executor)
        {
            _teamMembers = _teamMembers.Append(executor);
            _users.Add(executor, AccessRights.Edit);
        }

        public void AddExecutorWithAccessRights(IExecutor executor, AccessRights accessRights)
        {
            _users.Add(executor, accessRights);
        }

        public void AddState(State state)
        {
            _states = _states.Append(state);
        }
    }
}