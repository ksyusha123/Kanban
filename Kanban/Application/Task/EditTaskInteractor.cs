using System;
using Domain;

namespace Application
{
    public class EditTaskInteractor
    {
        private readonly IRepository<Task> _taskRepository;

        public EditTaskInteractor(IRepository<Task> taskRepository) => _taskRepository = taskRepository;

        public async System.Threading.Tasks.Task EditTaskAsync(Guid taskId, string name = null, string description = null)
        {
            var task = await _taskRepository.GetAsync(taskId);

            if (name != null) task.Name = name;

            if (description != null) task.Description = description;
            
            await _taskRepository.UpdateAsync(task);
        }
    }
}