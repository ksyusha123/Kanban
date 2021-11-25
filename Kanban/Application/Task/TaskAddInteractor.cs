using System;
using Domain;
using Task = System.Threading.Tasks.Task;

namespace Application
{
    public class TaskAddInteractor
    {
        private readonly IRepository<IExecutor> executorRepository;
        private readonly IRepository<ITask> taskRepository;

        public TaskAddInteractor(IRepository<IExecutor> executorRepository, IRepository<ITask> taskRepository)
        {
            this.executorRepository = executorRepository;
            this.taskRepository = taskRepository;
        }

        public async Task AssignAsync(Guid executorId, Guid taskId)
        {
            var executor = await executorRepository.GetAsync(executorId);
            var task = await taskRepository.GetAsync(taskId);

            task.Executor = executor;
            await taskRepository.UpdateAsync(task);
        }
    }
}