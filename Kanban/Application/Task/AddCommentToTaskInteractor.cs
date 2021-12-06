using System;
using Domain;

namespace Application
{
    public class AddCommentToTaskInteractor
    {
        private readonly IRepository<Task, Guid> _taskRepository;

        public AddCommentToTaskInteractor(IRepository<Task, Guid> taskRepository) => _taskRepository = taskRepository;

        public async System.Threading.Tasks.Task AddCommentToTaskAsync(Guid taskId, Comment comment)
        {
            var task = await _taskRepository.GetAsync(taskId);
            task.AddComment(comment);
            await _taskRepository.UpdateAsync(task);
        }
    }
}