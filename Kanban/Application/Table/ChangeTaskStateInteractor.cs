using System;
using Domain;

namespace Application
{
    public class ChangeTaskStateInteractor
    {
        private readonly IRepository<Task, Guid> _taskRepository;

        public ChangeTaskStateInteractor(IRepository<Task, Guid> taskRepository) => _taskRepository = taskRepository;

        public async System.Threading.Tasks.Task ChangeTaskState(Guid taskId, State state)
        {
            var task = await _taskRepository.GetAsync(taskId);
            task.State = state;
            
            await _taskRepository.UpdateAsync(task);
        }
    }
}