using System;
using Domain;

namespace Application
{
    public class UnassignExecutorInteractor
    {
        private readonly IRepository<Task> _taskRepository;

        public UnassignExecutorInteractor(IRepository<Task> taskRepository) =>
            _taskRepository = taskRepository;

        public async System.Threading.Tasks.Task AssignAsync(Guid taskId)
        {
            var task = await _taskRepository.GetAsync(taskId);
            task.Executor = null;
            await _taskRepository.UpdateAsync(task);
        }
    }
}