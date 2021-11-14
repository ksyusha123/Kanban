using System;
using Domain;
using Task = System.Threading.Tasks.Task;

namespace Application
{
    public class TaskAddInteractor
    {
        private readonly IRepository<IExecutor> _executorRepository;
        private readonly IRepository<ITask> _taskRepository;

        public TaskAddInteractor(IRepository<IExecutor> executorRepository, IRepository<ITask> taskRepository) =>
            (_executorRepository, _taskRepository) = (executorRepository, taskRepository);

        public async Task AssignAsync(Guid executorId, Guid taskId)
        {
            var executor = await _executorRepository.GetAsync(executorId);
            var task = await _taskRepository.GetAsync(taskId);

            task.Executor = executor;
            await _taskRepository.UpdateAsync(task);
        }
    }
}