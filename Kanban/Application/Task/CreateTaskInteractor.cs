using System;
using Domain;

namespace Application
{
    public class CreateTaskInteractor
    {
        private readonly IRepository<Task, Guid> _taskRepository;
        public CreateTaskInteractor(IRepository<Task, Guid> taskRepository) => _taskRepository = taskRepository;

        public async System.Threading.Tasks.Task CreateTaskAsync(Task task)
        {
            await _taskRepository.AddAsync(task);
        }
    }
}