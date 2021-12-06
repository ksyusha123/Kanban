using System;
using Domain;

namespace Application
{
    public class AssignExecutorToTaskInteractor
    {
        private readonly IRepository<Executor, Guid> _executorRepository;
        private readonly IRepository<Task, Guid> _taskRepository;

        public AssignExecutorToTaskInteractor(IRepository<Executor, Guid> executorRepository, IRepository<Task, Guid> taskRepository) =>
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