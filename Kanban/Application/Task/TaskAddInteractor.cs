using System;
using Domain;

namespace Application
{
    public class TaskAddInteractor
    {
        private readonly IRepository<Executor> _executorRepository;
        private readonly IRepository<Task> _taskRepository;

        public TaskAddInteractor(IRepository<Executor> executorRepository, IRepository<Task> taskRepository) =>
            (_executorRepository, _taskRepository) = (executorRepository, taskRepository);

        public async System.Threading.Tasks.Task AssignAsync(Guid executorId, Guid taskId)
        {
            var executor = await _executorRepository.GetAsync(executorId);
            var task = await _taskRepository.GetAsync(taskId);

            task.Executor = executor;
            await _taskRepository.UpdateAsync(task);
        }
    }
}